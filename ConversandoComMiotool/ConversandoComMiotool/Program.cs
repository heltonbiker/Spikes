using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTD2XX_NET;
using static FTD2XX_NET.FTDI;

namespace ConversandoComMiotool
{
	class Program
	{
		static void Main(string[] args)
		{
			var ftdi = new FTDI();

			uint numdevices = 0;

			ftdi.GetNumberOfDevices(ref numdevices);

			var nodes = new FT_DEVICE_INFO_NODE[numdevices];

			ftdi.GetDeviceList(nodes);

			var status = ftdi.OpenByDescription("MioTreco");

			uint bytesWritten = 0;

			ftdi.Write(new byte[] { 1, 2, 3, 4 }, 4, ref bytesWritten);

			byte[] readBytes = new byte[4];
			uint bytesRead = 0;
			var res = ftdi.Read(readBytes, (uint)readBytes.Length, ref bytesRead);

			Console.WriteLine(res);

			//if (nodes.SingleOrDefault(node => node.Description == "MioTreco") is FT_DEVICE_INFO_NODE miotreco)
			//{
			//	var status = ftdi.o
			//}

			Console.ReadKey();
		}
	}
}
