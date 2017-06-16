using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SensitronicsSerialPort
{
	class Program
	{
		static void Main(string[] args)
		{
			//var searcher = new ManagementObjectSearcher("Select * from Win32_SerialPort");

			//ManagementObjectCollection manObjs = searcher.Get();

			//foreach (ManagementObject manObj in manObjs)
			//{
			//	var deviceId = manObj["PNPDeviceID"].ToString();

			//	Console.ReadKey();
			//}

			var serial = new SerialPort("COM1", 115200, Parity.None, 8, StopBits.One);
			serial.Open();
			
			while(true)
			{
				if (serial.BytesToRead > 0)
					Console.WriteLine(serial.ReadByte());
			}

			Console.ReadKey();
		}
	}
}
