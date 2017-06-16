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
using System.Windows.Shapes;

namespace TransparentDialog
{
	/// <summary>
	/// Interaction logic for ModalWindow.xaml
	/// </summary>
	public partial class ModalWindow : Window
	{
		public ModalWindow()
		{
			InitializeComponent();

			PreviewKeyDown += ModalWindow_PreviewKeyDown;
		}

		private void ModalWindow_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Close();
		}
	}
}
