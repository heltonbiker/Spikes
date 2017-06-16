using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using FTD2XX_NET;

namespace NewMiotoolSerial
{
	class Program
	{

		
		// USB\VID_0403&PID_6001\A902UI89		 
		

		static void Main(string[] args)
		{

			FTDI ftdi = new FTDI();
			FTDI.FT_STATUS status;

			uint bufferLenght = 0;
			status = ftdi.GetNumberOfDevices(ref bufferLenght);
			if (status != FTDI.FT_STATUS.FT_OK)
				return;

			FTDI.FT_DEVICE_INFO_NODE[] nodes = new FTDI.FT_DEVICE_INFO_NODE[bufferLenght];
			status = ftdi.GetDeviceList(nodes);
			if (status != FTDI.FT_STATUS.FT_OK)
				return;

			foreach (var node in nodes)
			{
				IntPtr handle = new IntPtr(node.GetHashCode());

				status = ftdi.OpenByLocation(node.LocId);
				if (status != FTDI.FT_STATUS.FT_OK)
					return;
				status = ftdi.SetBaudRate(230400);
				if (status != FTDI.FT_STATUS.FT_OK)
					return;
				status = ftdi.SetDataCharacteristics(
					FTDI.FT_DATA_BITS.FT_BITS_7,
					FTDI.FT_STOP_BITS.FT_STOP_BITS_1,
					FTDI.FT_PARITY.FT_PARITY_NONE
				);				
				if (status != FTDI.FT_STATUS.FT_OK)
					return;
				status = ftdi.SetDTR(false);
				if (status != FTDI.FT_STATUS.FT_OK)
					return;
				status = ftdi.SetRTS(false);
				if (status != FTDI.FT_STATUS.FT_OK)
					return;
				status = ftdi.SetTimeouts(1000, 1000);
				if (status != FTDI.FT_STATUS.FT_OK)
					return;


				status = ftdi.SetLatency(1);

				if (status != FTDI.FT_STATUS.FT_OK)
					return;


				status = ftdi.SetFlowControl(FTDI.FT_FLOW_CONTROL.FT_FLOW_NONE, 0, 0);
				if (status != FTDI.FT_STATUS.FT_OK)
					return;

			}

			Console.ReadLine();


			//foreach (var port in SerialPort.GetPortNames())
			//{
			//	Console.WriteLine(port);
			//}



			//const string VID = "VID_0403";
			//const string PID = "PID_6001";

			//var searcher = new ManagementObjectSearcher("Select * from Win32_USBHub");

			//ManagementObjectCollection manObjs = searcher.Get();

			//foreach (ManagementObject manObj in manObjs)
			//{
			//	var deviceId = manObj["DeviceID"].ToString();

			//	if (deviceId.Contains(VID) && deviceId.Contains(PID))
			//	{
			//		foreach (var prop in manObj.Properties)
			//		{
			//			Console.WriteLine($"{prop.Name}, {prop.Value}, {prop.Origin}");
			//		}
			//	}
			//}
			//Console.ReadLine();

		}
	}
}
