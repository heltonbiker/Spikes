using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Threading;
using Emgu.CV;
using Microsoft.Expression.Encoder.Devices;

namespace HelloWebCam
{
	class Program
	{
		static void Main(string[] args)
		{
			HelloAForge();

			//HelloExpressionEncode();

			//HelloEmguCV();

			Console.ReadKey();
		}

		private static void HelloEmguCV()
		{
			//Capture _capture = null; //Camera
			//bool _captureInProgress = false; //Variable to track camera state
			//int CameraDevice = 0; //Variable to track camera device selected
			////VideoDevice[] WebCams;

			//var emgu = 
	}

		private static void HelloExpressionEncode()
		{
			var foundDevices = EncoderDevices.FindDevices(EncoderDeviceType.Video);

			foreach (var device in foundDevices)
			{
				Console.WriteLine(device.Name);
				Console.WriteLine(device.Category);
				Console.WriteLine(device.DevicePath);


				Console.WriteLine();
			}
		}

		private static void HelloAForge()
		{
			var foundDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

			for (int i = 0; i < foundDevices.Count; i++)
			{
				var device = foundDevices[i];
				Console.WriteLine(device.Name);
			}

			var primeira = new VideoCaptureDevice(foundDevices[0].MonikerString);

			primeira.NewFrame += (sender, args) =>
			{
				Console.WriteLine(args.Frame.ToString());
			};

			primeira.Start();

			//Thread.Sleep(1000);

			//primeira.Stop();
			//primeira.WaitForStop();

			Console.ReadKey();
		}
	}
}
