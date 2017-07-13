//using Accord.Video;
//using Accord.Video.DirectShow;
//using Accord.Imaging.Filters;
using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Drawing.Imaging;
using Accord.Video;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using Emgu;
using Emgu.CV;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;

namespace HelloAforgeDeviceCapabilities
{
	class Program
	{
		static void Main(string[] args)
		{
			var tester = new CameraTester();

			//tester.RunAccord();

			//tester.RunDirectShow();

			tester.RunEMGU();

			//tester.RunExpressionEncoder();

			Console.ReadKey();
		}
	}

	class CameraTester
	{
		public List<long> Timestamps = new List<long>();

		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		//BrightnessCorrection _brilho = new BrightnessCorrection(80);

		//internal void RunAccord()
		//{
		//	var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

		//	VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);


		//	//videoSource.GetCameraProperty(CameraControlProperty.Exposure, 
		//	//							  out int exposure, 
		//	//							  out CameraControlFlags flags);

		//	//Console.WriteLine(exposure);
		//	//Console.WriteLine(flags);

		//	videoSource.GetCameraPropertyRange(CameraControlProperty.Exposure,
		//									   out int minVal, out int maxVal,
		//									   out int stepSize, out int defaultVal,
		//									   out CameraControlFlags exposureFlags);

		//	int ajusteExposição = 0;

		//	videoSource.SetCameraProperty(CameraControlProperty.Exposure, maxVal - stepSize * ajusteExposição, CameraControlFlags.Manual);


		//	videoSource.VideoResolution = videoSource.VideoCapabilities
		//											 .OrderByDescending(c => c.FrameSize.Width)
		//											 .First();

		//	string dir = Path.Combine(desktop, "frames");
		//	foreach (FileInfo file in new DirectoryInfo(dir).GetFiles())
		//	{
		//		file.Delete();
		//	}
		//	Directory.CreateDirectory(dir);

		//	var receivedFrames = Observable.FromEventPattern<NewFrameEventHandler, NewFrameEventArgs>(
		//		h => videoSource.NewFrame += h,
		//		h => videoSource.NewFrame -= h);

		//	receivedFrames.Timestamp().Subscribe(args => SaveFrame(args));

		//	videoSource.Start();

		//	while (videoSource.FramesReceived < 1)
		//		;

		//	Task.Delay(10000).Wait();

		//	videoSource.SignalToStop();

		//	while (videoSource.IsRunning)
		//		;

		//	File.WriteAllText(Path.Combine(desktop, "intervalos.txt"), 
		//					  String.Join(Environment.NewLine, Timestamps.Select(t => t.ToString())));

		//	Console.WriteLine("pronto");
		//}

		internal void RunDirectShow()
		{
			DsDevice[] systemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

			if (systemCameras.Length < 1)
				return;

			var camera = systemCameras.First();
		}

		internal void RunEMGU()
		{
			var camera = new Capture();
			var exposure = camera.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Exposure);
			camera.ImageGrabbed += Camera_ImageGrabbed;
			camera.Start();
			Task.Delay(500).Wait();
			camera.Stop();
		}

		internal void RunExpressionEncoder()
		{
			var videoDevices = EncoderDevices.FindDevices(EncoderDeviceType.Video);
			var firstDevice = videoDevices.First();
			var job = new LiveJob();
			var deviceSource = job.AddDeviceSource(firstDevice, null);
			
		}

		private void Camera_ImageGrabbed(object sender, EventArgs e)
		{
			var camera = sender as Capture;
			//var foo = OutputArray.GetEmpty();
			//var retrieved = camera.Retrieve(foo as IOutputArray);
			//Console.WriteLine((foo as IOutputArray).GetOutputArray().GetSize());
		}

		private void SaveFrame(Timestamped<EventPattern<NewFrameEventArgs>> args)
		{
			var ticks = args.Timestamp.DateTime.Ticks;

			var frame = args.Value.EventArgs.Frame as Bitmap;
			frame.RotateFlip(RotateFlipType.Rotate270FlipNone);
			//_brilho.ApplyInPlace(frame);
			

			var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);

			var encParams = new EncoderParameters()
			{
				Param = new[] 
				{
					new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L)   //// CONFIG
				}
			};			

			frame?.Save(Path.Combine(desktop, "frames", $"{ticks}.jpg"), encoder, encParams);
		}
	}
}
