using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataContextChanged
{
	class ViewModel : ObservableObject
	{


		public Coiso Coiso
		{
			get { return _coiso; }
			set
			{
				_coiso = value;
				RaisePropertyChanged(() => Coiso);
			}
		}
		Coiso _coiso = new Coiso();






		public ICommand ComandoCoisar
			=> new RelayCommand(Coisar);
		void Coisar()
		{
			Coiso = new Coiso() { Coisito = DateTime.Now.ToString() };
		}





	}
}
