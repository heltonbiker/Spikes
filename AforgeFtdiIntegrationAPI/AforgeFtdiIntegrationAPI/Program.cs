using System;
using FTD2XX_NET;
using static FTD2XX_NET.FTDI;
using System.Threading;
using System.Threading.Tasks;
using Accord.Video.DirectShow;
using System.Reactive.Linq;
using System.Drawing;
using System.Reactive;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace AforgeFtdiIntegrationAPI
{

	class Program
	{
		static void Main(string[] args)
		{
			foreach(var implementation in new API[] 
			{
				new CameraImplementation(),
				new FtdiImplementation()
			})
			{
				IEnumerable<IDispositivo> disponíveis = implementation.ListarDisponíveis();

				IDispositivo dispositivo = disponíveis.FirstOrDefault();
				if (dispositivo == null)
					return;

				try
				{
					dispositivo.Open();
				}
				catch { return; }

				//IDisposable subscription = dispositivo.AssinaNovosDados(d => ProcessarDados(d));
				Observable.FromEventPattern<object>(dispositivo, "NovosDados")
						  .Subscribe(obj => ProcessaNovosDados(obj));
			}
		}

		private static object ProcessaNovosDados(EventPattern<object> obj)
		{
			throw new NotImplementedException();
		}


		private static void RunAccordActivation()
		{
			// cria agente da biblioteca
			// *

			// cria lista com dispositivos
			if (videoCollection.Count > 1)
				return;

			// seleciona o primeiro dispositivo
			var device = videoCollection[0];

			// abre conexão

			// escreve alguma coisa
			videodevice.SetCameraProperty(CameraControlProperty.Exposure, 0, CameraControlFlags.None);

			// assina evento recebimento

			// fica lendo evento
			// *
		}

		private static void Processa(EventPattern<Bitmap> bmp)
		{
			Bitmap frame = bmp.EventArgs.Clone() as Bitmap;
		}
	}

	abstract class API
	{
		internal abstract IEnumerable<IDispositivo> ListarDisponíveis();
	}

	class CameraImplementation : API
	{
		internal override IEnumerable<IDispositivo> ListarDisponíveis()
		{
			var videoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			var list = new List<IDispositivo>();
			foreach (var filter in videoCollection)
			{
				list.Add(new DispositivoAccord(filter as FilterInfo));
			}
			return list;
		}
	}

	class FtdiImplementation : API
	{
		FTDI _ftdi = new FTDI();

		internal override IEnumerable<IDispositivo> ListarDisponíveis()
		{
			var defaultResult = Enumerable.Empty<IDispositivo>();

			// cria lista com dispositivos
			uint count = 0;
			if (_ftdi.GetNumberOfDevices(ref count) != FT_STATUS.FT_OK)
			{
				return defaultResult;
			}

			FT_DEVICE_INFO_NODE[] deviceList = new FT_DEVICE_INFO_NODE[count];
			if (_ftdi.GetDeviceList(deviceList) != FT_STATUS.FT_OK)
			{
				return defaultResult;
			}

			return deviceList.Select(node => new DispositivoFtdi(node));
		}
	}

	interface IDispositivo
	{
		void Open();		
		void Close();
	}

	interface IDispositivo<TFrame> : IDispositivo
	{
		IDisposable AssinaNovosDados(Action<TFrame> p);
	}

	internal class DispositivoFtdi : IDispositivo<byte[]>
	{
		FTDI _ftdi = new FTDI();
		FT_DEVICE_INFO_NODE _node;

		public DispositivoFtdi(FT_DEVICE_INFO_NODE node)
		{
			_node = node;
		}

		public void Open()
		{
			// abre conexão
			var status = _ftdi.OpenBySerialNumber(_node.SerialNumber);
			if (status != FT_STATUS.FT_OK)
			{
				throw new InvalidOperationException($"Tentativa de abrir dispositivo {_node.SerialNumber} retornou status {status}");
			}
		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public IDisposable AssinaNovosDados(Action<byte[]> p)
		{
			// assina evento recebimento
			AutoResetEvent receivedDataEvent = new AutoResetEvent(false);
			_ftdi.SetEventNotification(FT_EVENTS.FT_EVENT_RXCHAR, receivedDataEvent);

			// fica lendo evento
			CancellationTokenSource _cancellation = new CancellationTokenSource();
			Task.Run(() =>
			{
				while (!_cancellation.IsCancellationRequested)
				{
					receivedDataEvent.WaitOne();

					uint nrOfBytesAvailable = 0;
					if (_ftdi.GetRxBytesAvailable(ref nrOfBytesAvailable) != FT_STATUS.FT_OK)
					{
						continue;
					}
					if (nrOfBytesAvailable > 0)
					{
						byte[] rawBytes = new byte[nrOfBytesAvailable];
						uint numBytesRead = 0;
						if (_ftdi.Read(rawBytes, nrOfBytesAvailable, ref numBytesRead) == FT_STATUS.FT_OK)
						{
							// fazer algo com os rawBytes
						}
					}
				}
			}, _cancellation.Token);

			return Disposable.Create(() => _cancellation.Cancel());
		}
	}

	internal class DispositivoAccord : IDispositivo<Bitmap>
	{
		VideoCaptureDevice _videoDevice;

		public DispositivoAccord(FilterInfo filterinfo)
		{
			_videoDevice = new VideoCaptureDevice(filterinfo.MonikerString);
		}


		public IDisposable AssinaNovosDados(Action<Bitmap> p)
		{
			var subscription = Observable.FromEventPattern<Bitmap>(_videoDevice, "NewFrame")
										 .Subscribe(bmp => ProcessaNovosDados(bmp));
			return Disposable.Create(() => subscription.Dispose());

		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public void Open()
		{
		}
	}


}
