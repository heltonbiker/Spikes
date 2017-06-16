using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImagePlaceholderColorBar
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();

			DataContext = this;
		}

        public BitmapSource Colorbar
        {
            get
            {
                var resultado = new RenderTargetBitmap(20,255,96,96,PixelFormats.Pbgra32);

                var dv = new DrawingVisual();

                using (var dc = dv.RenderOpen())
                {
                    dc.DrawRectangle(Brushes.Blue, null, new Rect(5, 5, 10, 10));
                }

                resultado.Render(dv);

                return resultado;
            }
        }
	}
}