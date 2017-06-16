using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video.DirectShow;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Collections.ObjectModel;

namespace HelloWebCamWpf
{
	public class LiveCameraViewModel : ObservableObject
	{
		public ImageSource FrameSource
		{
			get { return _frameSource; }
			set
			{
				_frameSource = value;
				RaisePropertyChanged(() => FrameSource);
			}
		}
		ImageSource _frameSource;



		public ObservableCollection<System.Windows.Point> Blobs { get; set; }
			= new ObservableCollection<System.Windows.Point>();



		// CONSTRUTOR
		public LiveCameraViewModel(String monikerString)
		{
			var capturedevice = new VideoCaptureDevice(monikerString);

			for (int i = 0; i < capturedevice.VideoCapabilities.Length; i++)
			{
				Console.WriteLine(capturedevice.VideoCapabilities[0].AverageFrameRate);
			}			

			capturedevice.NewFrame += NewFrameHandler;

			capturedevice.Start();
		}

		BlobCounter blobCounter = new BlobCounter();

		private void NewFrameHandler(object sender, AForge.Video.NewFrameEventArgs eventArgs)
		{
			var bitmap = eventArgs.Frame;

			var grayfilter = new Grayscale(0.2125, 0.7154, 0.0721);
			var thresholdFilter = new Threshold(100);
			var dilationFilter = new Dilatation();
			var torgb = new GrayscaleToRGB();

			bitmap = grayfilter.Apply(bitmap);
			bitmap = thresholdFilter.Apply(bitmap);
			bitmap = dilationFilter.Apply(bitmap);
			bitmap = torgb.Apply(bitmap);

			var blobBmp = bitmap.Clone() as Bitmap;

			Task.Run(() => DetectBlobs(blobBmp));

			var displayBmp = bitmap.Clone() as Bitmap;

			Task.Run(() => DisplayBitmap(displayBmp));
		}

		void DetectBlobs(Bitmap bitmap)
		{
			var result = new List<System.Windows.Point>();

			try
			{
				blobCounter.ProcessImage(bitmap);

				var blobs = blobCounter.GetObjectsInformation();

				foreach (var blob in blobs)
				{
					AForge.Point center = blob.CenterOfGravity;
					result.Add(new System.Windows.Point(center.X, center.Y));
				}

				//Console.WriteLine(result.Count);

				Application.Current.Dispatcher.Invoke(() =>
				{
					Blobs.Clear();

					foreach (var pt in result)
					{
						Blobs.Add(pt);
					}
				});
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

		}

		private void DisplayBitmap(Bitmap bitmap)
		{
			var bitmapData = bitmap.LockBits(
				new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
				System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

			var bitmapSource = BitmapSource.Create(
				bitmapData.Width, bitmapData.Height, 96, 96, PixelFormats.Bgr24, null,
				bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

			bitmap.UnlockBits(bitmapData);

			bitmapSource.Freeze();

			FrameSource = bitmapSource;
		}
	}
}
