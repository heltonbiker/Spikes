using Miotec.BioSinais.ModeloDomínio;
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

namespace ViewportSuperiorDemo
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{

		//public double Início
		//{
		//	get { return _início; }
		//	set
		//	{
		//		_início = value;
		//		RaisePropertyChanged("Início");
		//		RaisePropertyChanged("IntervaloHorizontal");
		//	}
		//}
		//double _início = 0.25;

		//public double Fim
		//{
		//	get { return _fim; }
		//	set
		//	{
		//		_fim = value;
		//		RaisePropertyChanged("Fim");
		//		RaisePropertyChanged("IntervaloHorizontal");
		//	}
		//}
		//double _fim = 0.75;

		public Intervalo IntervaloHorizontal
		{
			get { return _intervalo; }
			set
			{
				_intervalo = value;
				RaisePropertyChanged("IntervaloHorizontal");
			}
		}
		Intervalo _intervalo = new Intervalo(0.25, 0.75);


		// CONSTRUTOR
		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;
		}


		public event PropertyChangedEventHandler PropertyChanged;
		private void RaisePropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}


	}
}
