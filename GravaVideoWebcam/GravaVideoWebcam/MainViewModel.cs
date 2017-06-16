using Accord.Video;
using Accord.Video.DirectShow;
using Accord.Video.FFMPEG;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GravaVideoWebcam
{
	public class MainViewModel : ViewModelBase, IDisposable
	{
		VideoCaptureDevice _videoCaptureDevice;
		VideoFileWriter _videoWriter = new VideoFileWriter();

		public BitmapImage FrameSource
		{
			get { return _frameSource; }
			set
			{
				_frameSource = value;
				RaisePropertyChanged(() => FrameSource);
			}
		}
		BitmapImage _frameSource;



		// CONSTRUTOR
		public MainViewModel()
		{
			var foundDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

			var logitechs = new List<FilterInfo>();

			for (int i = 0; i < foundDevices.Count; i++)
			{
				var device = foundDevices[i];
				if (device.Name.Contains("ogitech"))
					logitechs.Add(device);
			}

			var logitech = logitechs.SingleOrDefault();

			if (logitech != null)
			{
				_videoCaptureDevice = new VideoCaptureDevice(logitech.MonikerString);

				_videoCaptureDevice.Start();

				Thread.Sleep(300);

				_videoCaptureDevice.NewFrame += NewFrameHandler;
			}

		}

		CancellationTokenSource _tokenSource = new CancellationTokenSource();

		void NewFrameHandler(object sender, NewFrameEventArgs eventArgs)
		{
			var bitmap = eventArgs.Frame;

			var displayBmp = bitmap.Clone() as Bitmap;

			Task.Run(() => DisplayBitmap(displayBmp), _tokenSource.Token);

			IniciaGravação(bitmap);

			try
			{
				_videoWriter.WriteVideoFrame(bitmap, sw.Elapsed);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}


		Stopwatch sw;

		bool _gravando = false;

		void IniciaGravação(Bitmap bitmap)
		{
			if (_gravando)
				return;

			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "video.avi");
			_videoWriter.Open(path, bitmap.Width, bitmap.Height);
			sw = Stopwatch.StartNew();
			_gravando = true;
		}

		void DisplayBitmap(Bitmap bitmap)
		{
			using (var stream = new MemoryStream())
			{
				bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
				stream.Position = 0;

				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.StreamSource = stream;
				bitmapImage.EndInit();

				bitmapImage.Freeze();

				FrameSource = bitmapImage;
			}
		}

		public void Dispose()
		{
			_tokenSource.Cancel();
			_tokenSource.Token.WaitHandle.WaitOne();

			if (_videoCaptureDevice != null)
			{
				_videoCaptureDevice.NewFrame -= NewFrameHandler;
				_videoCaptureDevice.Stop();
			}

			base.Cleanup();
		}
	}
}
