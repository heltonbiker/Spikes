using System;
using System.Collections.Generic;
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

namespace TransparentDialog
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Window modal;

		public MainWindow()
		{
			InitializeComponent();

			Loaded += (a, b) => ShowModal();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ShowModal();
		}

		void ShowModal()
		{
			var modal = new ModalWindow()
			{
				DataContext = new ExemploConteúdoDiálogo()
			};

			modal.ShowDialog();
		}
	}
}
