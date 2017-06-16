using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNascimentoMultiCampos
{
	public class DateTimeViewModel : ObservableObject
	{
		private DateTime _dateTime;

		public string Day
		{
			get { return _day; }
			set
			{
				_day = value;
				RaisePropertyChanged(() => Day);
			}
		}
		string _day;

		public string Month
		{
			get { return _month; }
			set
			{
				_month = value;
				RaisePropertyChanged(() => Month);
			}
		}
		string _month;

		public string Year
		{
			get { return _year; }
			set
			{
				_year = value;
				RaisePropertyChanged(() => Year);
			}
		}
		string _year;


		public DateTimeViewModel(DateTime dateTime)
		{
			_dateTime = dateTime;
		}
	}
}
