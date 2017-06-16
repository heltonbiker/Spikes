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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TesteMarcadoresView
{
	public partial class MarcadoresView : UserControl
	{
		public MarcadoresView()
		{
			this.InitializeComponent();
		}

		private void MarcadoresMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{			
            double x = e.GetPosition(ContainerImagem).X;
            double y = e.GetPosition(ContainerImagem).Y;

            double w = ContainerImagem.ActualWidth;
            double h = ContainerImagem.ActualHeight;

            double W = ImagemMarcadores.Source.Width;
            double H = ImagemMarcadores.Source.Height;

            double xscale = W/w;
            double yscale = H/h;

            double xnorm = x/w;
            double ynorm = y/h;

            Canvas.SetLeft(CirculoCursor, x*xscale);
            Canvas.SetTop(CirculoCursor, y*yscale);

            

            //Console.WriteLine(string.Format("{0}, {1}", x,y));

            //pai.PegaPosicao(e.GetPosition(this).X, e.GetPosition(this).Y);
		}

	}
}