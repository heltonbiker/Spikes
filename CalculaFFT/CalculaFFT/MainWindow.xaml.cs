using System.Windows;

namespace CalculaFFT
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Loaded += (a, b) => { DataContext = new MainViewModel(); };
		}
	}
}
