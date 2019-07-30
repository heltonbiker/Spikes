using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MultiRangeSliderWithGridSplitter
{
	public class ViewModel : ReactiveObject
	{
		[Reactive] public double Inicio { get; set; }
		[Reactive] public double L1 { get; set; }
		[Reactive] public double L2 { get; set; }
		[Reactive] public double Fim { get; set; }
	}
}