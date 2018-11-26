using Accord.Video;
using Accord.Video.DirectShow;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CameraDirectShowAPISandbox
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		 VideoCaptureDevice _camera;


		public BitmapSource ImageSource
		{
			get => _imageSource;
			set

			{
				_imageSource = value;
				RaisePropertyChanged();
			}
		}
		BitmapSource _imageSource;


		public IEnumerable<VideoCapabilities> Capabilities 
			=> _camera.VideoCapabilities;


		public VideoCapabilities CapabilitiesSelecionada
		{
			get => _capabilitiesSelecionada;
			set
			{
				_capabilitiesSelecionada = value;
				AplicaCapability();
				RaisePropertyChanged();
			}
		}
		VideoCapabilities _capabilitiesSelecionada;


		public int Exposicao
		{
			get => _exposicao;
			set
			{
				_exposicao = value;
				SetarExposicao();
				RaisePropertyChanged();
			}
		}
		int _exposicao;


		public int ExposicaoLida
		{
			get => _exposicaoLida;
			set
			{
				_exposicaoLida = value;
				RaisePropertyChanged();
			}
		}
		int _exposicaoLida;


		public int MinValue
		{
			get => _minValue;
			set
			{
				_minValue = value;
				RaisePropertyChanged();
			}
		}
		int _minValue;

		public int MaxValue
		{
			get => _maxValue;
			set
			{
				_maxValue = value;
				RaisePropertyChanged();
			}
		}
		int _maxValue;






		// CONSTRUTOR
		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;

			Inicializa();

			Closing += (a, b) => _camera?.SignalToStop();
		}






		void Inicializa()
		{
			var filterinfo = new FilterInfoCollection(FilterCategory.VideoInputDevice)
				.FirstOrDefault();

			if (filterinfo == null)
				return;

			_camera = new VideoCaptureDevice(filterinfo.MonikerString);

			_camera.DisplayPropertyPage();

			return;


			Observable.FromEventPattern<NewFrameEventHandler, NewFrameEventArgs>(
					h => _camera.NewFrame += h,
					h => _camera.NewFrame -= h)
				.Subscribe(NovoFrameHandler);

			InicializaExposicao();

			DesativaFoco();

			CapabilitiesSelecionada = _camera.VideoResolution;
		}



		void InicializaExposicao()
		{
			_camera.GetCameraPropertyRange(CameraControlProperty.Exposure,
				out var minValue, out var maxValue,
				out var stepSize, out var defaultValue,
				out var flags);

			MinValue = minValue;
			MaxValue = maxValue;
		}

		void DesativaFoco()
		{
			_camera.GetCameraPropertyRange(CameraControlProperty.Focus,
				out var minValue, out var maxValue,
				out var _, out var _, out var flags);

			_camera.SetCameraProperty(CameraControlProperty.Focus, minValue, CameraControlFlags.Manual);
		}


		void NovoFrameHandler(EventPattern<NewFrameEventArgs> evt)
		{
			if (evt.EventArgs.Frame.Clone() is Bitmap bitmap)
			{
				using (var stream = new MemoryStream())
				{
					bitmap.Save(stream, ImageFormat.Bmp);
					stream.Position = 0;
					var bitmapimage = new BitmapImage();
					bitmapimage.BeginInit();
					bitmapimage.StreamSource = stream;
					bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapimage.EndInit();

					bitmapimage.Freeze();

					ImageSource = bitmapimage;
				}
			}
		}


		void SetarExposicao()
		{
			_camera.SetCameraProperty(CameraControlProperty.Exposure, Exposicao, CameraControlFlags.Manual);

			_camera.GetCameraProperty(CameraControlProperty.Exposure, out var value, out var flags);

			ExposicaoLida = value;
		}

		void AplicaCapability()
		{
			bool restart = false;

			if (_camera.IsRunning)
			{
				_camera.SignalToStop();
				_camera.WaitForStop();
				restart = true;
			}

			_camera.VideoResolution = CapabilitiesSelecionada;

			if (restart)
				_camera.Start();
		}




		public ICommand ComandoStartStop
			=> new RelayCommand<bool>(StartStop);
		void StartStop(bool toggled)
		{
			if (toggled)
			{
				_camera.Start();
			}
			else
				_camera.SignalToStop();
		}




		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
