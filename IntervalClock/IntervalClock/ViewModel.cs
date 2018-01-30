using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace IntervalClock
{
	public class ViewModel //: INotifyPropertyChanged
	{

		//public double TimeSeconds
		//{
		//	get { return _timeSeconds; }
		//	set
		//	{
		//		_timeSeconds = value;
		//		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TimeSeconds"));
		//	}
		//}
		//double _timeSeconds;


		//onst double MILLIS_INTERVAL = 10;

		public ViewModel()
		{
			//Observable.Interval(TimeSpan.FromMilliseconds(MILLIS_INTERVAL))
			//		  .Subscribe(token => Task.Run(() => TimeSeconds += MILLIS_INTERVAL / 1000));
		}


		//public event PropertyChangedEventHandler PropertyChanged;


		Stopwatch _sw = Stopwatch.StartNew();



		public string GetTime()
		{
			return (_sw.ElapsedMilliseconds / 1000.0).ToString("N3");
		}
	}
}
