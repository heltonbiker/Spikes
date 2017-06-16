using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Miotec.SocketCommunication
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public Point Position
		{
			get { return _position; }
			private set
			{
				_position = value;
				RaisePropertyChanged("Position");
			}
		}
		Point _position;

		TcpMessenger _messenger = new TcpMessenger();



		public MainWindow()
		{
			InitializeComponent();
			_messenger.Received += receiveMessage; 
		}

		private void receiveMessage(object sender, string message)
		{
			Point pos = JsonConvert.DeserializeObject<Point>(message);
			Position = pos;
		}

		private void Grid_MouseMove(object sender, MouseEventArgs e)
		{
			Position = e.GetPosition(EventArea);
			RaisePropertyChanged("Position");

			string jsonString = JsonConvert.SerializeObject(Position);
			_messenger.Send(jsonString);
		}





		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string v)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs("Position"));
		}
	}
}
