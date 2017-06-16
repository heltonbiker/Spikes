using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Miotec.SocketCommunication
{
	public class TcpMessenger : ISocketMessenger
	{
		int _porta_padrão = 555555;
		int _porta_utilizada;
		TcpClient _tcpClient;


		public TcpMessenger ()
		{
			_porta_utilizada = _porta_padrão;

			Task.Run(() => CommunicationLoop());
		}


		private void CommunicationLoop()
		{
			while (true)
			{
				Console.WriteLine("communicationloop");
				Thread.Sleep(300);
			}
		}




		public void Send(string message)
		{
			//throw new NotImplementedException();
		}

		public event EventHandler<string> Received;

		void OnReceived(string message)
		{
			if (Received != null)
				Received(this, message);
		}
	}
}
