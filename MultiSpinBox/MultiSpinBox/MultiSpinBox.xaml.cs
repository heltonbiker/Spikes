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

namespace MultiSpinBoxProj
{
	public partial class MultiSpinBox : UserControl
	{
		public MultiSpinBox()
		{
			InitializeComponent();

			Loaded += MultiSpinBox_Loaded;
		}

		void MultiSpinBox_Loaded(object sender, RoutedEventArgs e)
		{
			int items = Inteiras + Decimais;
			double menor = 1.0 / (10.0 * Decimais);
		}










		public int Decimais
		{
			get { return (int)GetValue(DecimaisProperty); }
			set { SetValue(DecimaisProperty, value); }
		}

		public static readonly DependencyProperty DecimaisProperty =
			DependencyProperty.Register("Decimais", typeof(int), typeof(MultiSpinBox), new PropertyMetadata(1));




		public int Inteiras
		{
			get { return (int)GetValue(InteirasProperty); }
			set { SetValue(InteirasProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Inteiras.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty InteirasProperty =
			DependencyProperty.Register("Inteiras", typeof(int), typeof(MultiSpinBox), new PropertyMetadata(2));


	}
}
