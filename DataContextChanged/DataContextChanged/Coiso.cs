using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContextChanged
{
	public class Coiso : ObservableObject
	{

		public string Coisito
		{
			get { return _coisito; }
			set
			{
				_coisito = value;
				RaisePropertyChanged(() => Coisito);
			}
		}
		string _coisito;

	}
}
