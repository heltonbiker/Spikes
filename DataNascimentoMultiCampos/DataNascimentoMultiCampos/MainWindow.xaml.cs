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

namespace DataNascimentoMultiCampos
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;

			var vinteanos = TimeSpan.FromDays(365 * 20);
			var vinteanosatrás = DateTime.Now - vinteanos;

			Paciente = new PacienteViewModel(new Paciente()
			{
				Nome = "Helton",
				DataNascimento = vinteanosatrás
			});
		}



		public PacienteViewModel Paciente
		{
			get { return _paciente; }
			set
			{
				_paciente = value;
				RaisePropertyChanged("Paciente");
			}
		}
		PacienteViewModel _paciente;




		public event PropertyChangedEventHandler PropertyChanged;

		void RaisePropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
