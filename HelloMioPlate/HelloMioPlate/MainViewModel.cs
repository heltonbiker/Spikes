using FTD2XX_NET;
using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FTD2XX_NET.FTDI;

namespace HelloMioPlate
{
	public class MainViewModel : ObservableObject, IDisposable
    {


		public string Mensagem => _mensagem.ToString();
		StringBuilder _mensagem = new StringBuilder();


		CancellationTokenSource _cancellation;
		FTDI _ftdi = new FTDI();


		public MainViewModel()
		{
			_cancellation = new CancellationTokenSource();
			Task.Run(() => Inicializar(), _cancellation.Token);
		}

		private void Inicializar()
		{
			var mioPlateNode = ListarDispositivos().Single(node => node.Description.Contains("MioPlate"));
			Console.WriteLine(_ftdi.OpenBySerialNumber(mioPlateNode.SerialNumber));
			Console.WriteLine(_ftdi.SetBaudRate(921600));

			var receivedDataEvent = new AutoResetEvent(false);
			Console.WriteLine(_ftdi.SetEventNotification(FT_EVENTS.FT_EVENT_RXCHAR, receivedDataEvent));

			while (!_cancellation.IsCancellationRequested)
			{
				receivedDataEvent.WaitOne();
				uint rxBytesAvailable = 0;
				_ftdi.GetRxBytesAvailable(ref rxBytesAvailable);
				if (rxBytesAvailable < 1)
					continue;
				byte[] bytes = new byte[rxBytesAvailable];
				uint numBytesRead = 0;
				_ftdi.Read(bytes, rxBytesAvailable, ref numBytesRead);
				if (_mensagem.Length > 100000)
					Console.WriteLine(_mensagem.ToString());
			}
		}

		private FT_DEVICE_INFO_NODE[] ListarDispositivos()
		{
			FTDI ftdi = new FTDI();
			FT_STATUS status;
			uint bufferLenght = 0;
			status = ftdi.GetNumberOfDevices(ref bufferLenght);
			FT_DEVICE_INFO_NODE[] result = new FT_DEVICE_INFO_NODE[bufferLenght];

			if (status != FT_STATUS.FT_OK)
				return null;

			status = ftdi.GetDeviceList(result);
			if (status != FT_STATUS.FT_OK)
				return null;

			return result;
		}

		public void Dispose()
		{
			_cancellation?.Cancel();
		}
	}
}
