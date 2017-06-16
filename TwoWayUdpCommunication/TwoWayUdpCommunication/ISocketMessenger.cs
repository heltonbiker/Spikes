using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotec.SocketCommunication
{
	public interface ISocketMessenger
	{
		void Send(string message);

		event EventHandler<string> Received;
	}
}
