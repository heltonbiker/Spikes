using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiRangeSliderWithGridSplitter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new ViewModel();
		}

		private void GridSplitter_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if (DataContext is ViewModel vm)
			{
				var vv = grid.ColumnDefinitions.Where((v,i) => i%2 == 0).Select(cd =>cd.ActualWidth).ToArray();
				var soma = vv.Sum();

				vm.Inicio = vv.Take(1).Sum() / soma;
				vm.L1 = vv.Take(2).Sum() / soma;
				vm.L2 = vv.Take(3).Sum() / soma;
				vm.Fim = vv.Take(4).Sum() / soma;
			}
		}
	}
}
