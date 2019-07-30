using MVVM.Tools;

namespace HelloJotConfig
{
	public class ViewModel : ObservableObject
	{
		public ViewModel()
		{
			ConfigurationService.Tracker.Track(this);
		}

		public int Value1
		{
			get => _value1;
			set
			{
				_value1 = value;
				RaisePropertyChangedEvent("Value1");
			}
		}
		int _value1;

		public int Value2
		{
			get => _value2;
			set
			{
				_value2 = value;
				RaisePropertyChangedEvent("Value2");
			}
		}
		int _value2;

	}
}