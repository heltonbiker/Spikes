using System;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MiotoolUsbExplorer
{
    public class MainViewModel : ReactiveObject
    {
        const int MiotoolFrameStart = 200;
        const int MiotoolFrameEnd = 206;
        const int MiotoolReceivedFrameStart = 205;
        const int MiotoolReceivedFrameEnd = 207;




        public ObservableCollection<string> NomesPorta { get; }
            = new ObservableCollection<string>();

        public ObservableCollection<SerialPort> Miotools { get; }
            = new ObservableCollection<SerialPort>();

        //public ObservableCollection<SerialPort> MiotoolsAtivos { get; }
        //    = new ObservableCollection<SerialPort>();

        [Reactive] public string SerialNumber { get; set; }

        [Reactive] public int[] Valores { get; set; }




        public ICommand ComandoListarPortas => ReactiveCommand.Create(ListarPortas);

        void ListarPortas()
        {
            NomesPorta.Clear();

            foreach (var porta in SerialPort.GetPortNames())
            {
                NomesPorta.Add(porta);
            }
        }


        public ICommand ComandoTestarPortas => ReactiveCommand.Create(TestarPortas);

        void TestarPortas()
        {
            Miotools.ToList().ForEach(p => p.Dispose());

            Miotools.Clear();

            foreach (var nomePorta in NomesPorta)
            {
                var porta = new SerialPort(nomePorta, 230400, Parity.None, 8, StopBits.One)
                {
                    RtsEnable = false,
                    DtrEnable = false,
                    ReadTimeout = 1000,
                    WriteTimeout = 1000
                };

                try
                {
                    porta.Open();

                    byte[] handshakebytes = CreateMessage(10);

                    porta.Write(handshakebytes, 0, handshakebytes.Length);

                    Task.Delay(100).Wait();

                    var response = new byte[10];

                    porta.Read(response, 0, response.Length);

                    var valid = IsValidFrame(response);

                    if (valid)
                    {
                        Miotools.Add(porta);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erro ao tentar porta {nomePorta}: {e.Message}");
                    porta.Dispose();
                }
                
            }
        }




        public ICommand ComandoCarregarDados => ReactiveCommand.Create(CarregarDados);

        void CarregarDados()
        {
            if (Miotools.FirstOrDefault() is SerialPort miotoolPort)
            {
                var serialMessage = CreateMessage(20);
                miotoolPort.Write(serialMessage, 0, serialMessage.Length);

                Task.Delay(100).Wait();

                var serialResponse = new byte[10];
                miotoolPort.Read(serialResponse, 0, serialResponse.Length);

                if (IsValidFrame(serialResponse))
                {
                    var serial = Decode(serialResponse.Skip(2).Take(4).ToArray());
                    SerialNumber = serial.ToString();
                    return;
                }
            }

            SerialNumber = string.Empty;
        }


        public ICommand ComandoIniciarCaptura => ReactiveCommand.Create(IniciarCaptura);

        void IniciarCaptura()
        {
            if (Miotools.FirstOrDefault() is SerialPort miotoolPort)
            {

            }
        }







        byte[] CreateMessage(byte code)
        {
            var result = new byte[10];

            result[0] = MiotoolFrameStart;

            result[1] = code;

           // Buffer.BlockCopy(data, 0, result, 2, data.Length);

            result[8] = MiotoolFrameEnd;

            result[9] = checksum(result);

            return result;
        }


        public static bool IsValidFrame(byte[] data)
        {

            if (data == null ||
                data.Length != 10 ||
                data[0] != MiotoolReceivedFrameStart ||
                data[8] != MiotoolReceivedFrameEnd)

                return false;


            var result = checksum(data) == data[9];

            return result;
        }


        static byte checksum(byte[] result)
        {
            // soma todos os valores exceto o último
            byte checksum = 0;
            for (var i = 0; i < result.Length - 1; i++)
                checksum += result[i];
            return checksum;
        }


        public static int Decode(byte[] data)
        {
            Array.Reverse(data);
            var numeroInflado = BitConverter.ToInt32(data, 0);
            if (numeroInflado < 0)
                return numeroInflado;
            var hexstring = numeroInflado.ToString("X");
            var numeroVerdadeiro = int.Parse(hexstring); // "X" é hexadecimal
            return numeroVerdadeiro;
        }

    }
}