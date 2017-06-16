using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiotoolUsbSerialPort
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				var ports = SerialPort.GetPortNames();

				if (ports.Length > 0)
				{
					Console.WriteLine($"Portas detectadas: {ports.Length}");

					foreach (var port in ports)
					{
						Console.WriteLine(port);

						try
						{
							var model = new MiotoolUsbModel(port);

							model.Connect();

							//var mioUsbPort = new MiotoolUsbPort(port);

							//if (!mioUsbPort.IsOpen)
							//	mioUsbPort.Open();

							//if (mioUsbPort.IsOpen)
							//{
							//	mioUsbPort.ExecuteCommand(MiotoolCommand.Connect);

							//	mioUsbPort.ExecuteCommand(MiotoolCommand.ReadBattery);

							//	mioUsbPort.ExecuteCommand(MiotoolCommand.ReadBattery);

							//	mioUsbPort.ExecuteCommand(MiotoolCommand.ReadModel);

							//	mioUsbPort.ExecuteCommand(MiotoolCommand.ReadVersion);

							//	mioUsbPort.ExecuteCommand(MiotoolCommand.ReadSerialNumber);

							//}

							Console.WriteLine($"Modelo: {model.ReadModel()}");
							Console.WriteLine($"Serial: {model.ReadSerialNumber()}");
							Console.WriteLine($"Bateria: {model.ReadBattery()}");


						}
						catch { }				
					}

					Console.ReadKey();
				}

				Thread.Sleep(500);

				//Console.Clear();
			}
		}

	}

	public class MiotoolUsbModel
	{
		MiotoolUsbPort _port;

		// CONSTRUTOR
		public MiotoolUsbModel(string portname)
		{
			_port = new MiotoolUsbPort(portname);
		}

		internal void Connect()
		{
			if (!_port.IsOpen)
				_port.Open();

			_port.ExecuteCommand(MiotoolCommand.Connect);
		}

		internal double ReadBattery()
		{
			var response = _port.ExecuteCommand(MiotoolCommand.ReadBattery);
			double val = BitConverter.ToUInt16(response, 2);
			double max = BitConverter.ToUInt16(response, 4);
			double min = BitConverter.ToUInt16(response, 5);

			//Console.WriteLine($"{max}; {min}; {val}");
			return 100 * (val - min) / (max - min);
        }

		internal string ReadModel()
		{
			var response = _port.ExecuteCommand(MiotoolCommand.ReadModel);
			var model = (ModelosMiotool)response[2];
			return model.ToString();
		}

		internal int ReadSerialNumber()
		{
			var array = _port.ExecuteCommand(MiotoolCommand.ReadSerialNumber);
			var destArray = new byte[4];
			Buffer.BlockCopy(array, 2, destArray, 0, 4);
			return ConversorNumeroSerial.Decode(destArray);
		}
	}


	public class MiotoolUsbPort : SerialPort
	{
		// CONSTRUTOR
		public MiotoolUsbPort(string port)
			: base(port, 230400, Parity.None, 8, StopBits.One)
		{
			RtsEnable = false;
			DtrEnable = false;
			ReadTimeout = 1000;
			WriteTimeout = 1000;
		}

		internal byte[] ExecuteCommand(MiotoolCommand command)
		{
			var commandArray = MiotoolFrame.MountFrame(command);
			Write(commandArray, 0, commandArray.Length);

			while (BytesToRead < 10)
				;

			byte[] response = new byte[10];
			Read(response, 0, 10);
			var byteValues = String.Join(";", response.Select(b => b.ToString()));
			//Console.WriteLine($"{command.ToString()} response = {byteValues} ({response.Length} read, {BytesToRead} bytes remaining).");

			return response;
		}
	}


	public static class MiotoolFrame
	{

		private const int MiotoolFrameStart = 200;
		private const int MiotoolFrameEnd = 206;
		private const int MiotoolReceivedFrameStart = 205;
		private const int MiotoolReceivedFrameEnd = 207;


		public static byte[] MountFrame(MiotoolCommand command, params byte[] data)
		{
			byte[] result = new byte[10];

			result[0] = MiotoolFrameStart;
			result[1] = (byte)command;
			result[8] = MiotoolFrameEnd;

			int j = 0;
			for (int i = 2; i < result.Length - 1; i++)
			{
				if (j >= data.Length)
					break;
				result[i] = data[j++];
			}

			byte checksum = 0;
			for (int i = 0; i < result.Length - 1; i++)
				checksum += result[i];
			result[9] = checksum;
			return result;
		}
	}



	public class ConversorNumeroSerial
	{

		/// <summary>
		/// Converte um array de byte[4] em um inteiro de uma forma bizarra.
		/// </summary>
		/// <remarks>
		/// A forma bizarra é a seguinte:
		///     - O número inteiro decorrente da conversão dos bytes está na verdade "inflado";
		///     - Deve-se obter a representação hexadecimal desse número inflado;
		///     - O número verdadeiro é o número decimal parseado a partir dessa representação;
		/// </remarks>
		public static int Decode(byte[] data)
		{
			Array.Reverse(data);
			int numero_inflado = BitConverter.ToInt32(data, 0);
			if (numero_inflado < 0)
				return numero_inflado;
			string hexstring = numero_inflado.ToString("X");
			var numero_verdadeiro = Int32.Parse(hexstring); // "X" é hexadecimal
			return numero_verdadeiro;
		}

		/// <summary>
		/// Converte um inteiro em um array byte[4] de uma forma bizarra.
		/// </summary>
		/// <remarks>
		/// A forma bizarra com que é feita a conversão é um legado que não
		/// chega a interferir no dia a dia de uso do aparelho, mas que
		/// requer que se considere isso para que as coisas dêem certo.
		/// Consiste no seguinte:
		///     - Obter a representação do número na forma de string;
		///     - Parsear essa string "como se ela fosse" hexadecimal;
		///     - Converter o número em um array de bytes
		/// </remarks>
		public static byte[] Encode(int valor)
		{
			int numero_verdadeiro = valor;
			string hexstring = valor.ToString();
			int numero_inflado = Int32.Parse(hexstring, System.Globalization.NumberStyles.HexNumber);
			byte[] array = BitConverter.GetBytes(numero_inflado);
			Array.Reverse(array);
			return array;
		}
	}



	public enum MiotoolCommand
	{
		Connect = 10,
		Disconnect = 11,
		ConnectionStatus = 12,
		ReadSerialNumber = 20,
		WriteSerialNumber = 21,
		ReadVersion = 30,
		ReadModel = 31,
		ReadMemory = 40,
		WriteMemory = 41,
		ReadBattery = 50,
		StartAcquisition = 60,
		StopAcquisition = 61,
		ConfirmStopAcquisition = 62,
		SyncDataAquisition = 63,
		WriteCalibration = 71,
		ReadCalibration = 72
	}

	public enum ModelosMiotool
	{
		[Description("Não Especificado")]
		NaoEspecificado = 0,

		[Description("Miotool USB 200 (2 Canais)")]
		USB200 = 1,

		[Description("Miotool USB 400(4 Canais)")]
		USB400 = 2,

		URO2 = 3,
		FONO2 = 4,
		USB800A = 5,
		USB800B = 6,
		FONO4 = 7,
		ODONTO2 = 8,
		ODONTO4 = 9

		// "Miotool USB 200 (2 Canais)",
		// "Miotool USB 400 (4 Canais)",
		// "Miotool Uro (2 Canais)",
		// "Miotool Fono (2 Canais)",
		// "Miotool USB 800 (4 + 4 Canais / Miotool A)",
		// "Miotool USB 800 (4 + 4 Canais / Miotool B)",
		// "Miotool Fono (4 Canais)",
		// "Miotool Odonto (2 Canais)",
		// "Miotool Odonto (4 Canais)"

	}
}

