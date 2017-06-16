using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;

namespace TesteMarcadoresView
{
	public partial class MarcadoresView : UserControl
	{
		
        EllipseGeometry _geometria_circulo;
        ScaleTransform _scale_transform;
        Point? _ponto_clicado;

        static double _escala = 5;
        static double _raio = 30;
        static double _espessura = 2;

        public double Escala { get { return _escala; } }
        public double Raio { get { return _raio; } }
        public double Espessura { get { return _espessura;} }

        public ObservableCollection<Point> ListaPontos { get; set; }
    

        // CONSTRUTOR
        public MarcadoresView()
		{
			this.InitializeComponent();

			ListaPontos = new ObservableCollection<Point>() {
				new Point(100,100),
				new Point(100,200),
				new Point(200,200)
			};
			
            _geometria_circulo = (EllipseGeometry)this.Resources["geometriacirculo"];
            _scale_transform = (ScaleTransform)this.Resources["transform"];

            AreaZoom.Visibility = Visibility.Hidden;

            DataContext = this;
		}


		private void MarcadoresMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{			
            double x = e.GetPosition(AreaEventos).X;
            double y = e.GetPosition(AreaEventos).Y;

            double w = AreaEventos.ActualWidth;
            double h = AreaEventos.ActualHeight;

            double W = ImagemOriginalMarcadores.Source.Width;
            double H = ImagemOriginalMarcadores.Source.Height;

            double xscale = W/w;
            double yscale = H/h;

            if (_ponto_clicado == null) {
                _geometria_circulo.Center = new Point(x,y);

                _scale_transform.CenterX = x;
                _scale_transform.CenterY = y;

                _scale_transform.ScaleX = 1;
                _scale_transform.ScaleY = 1;
            }

		}


		private void MouseEnter_TornaCirculoVisivel(object sender, System.Windows.Input.MouseEventArgs e) {
			AreaZoom.Visibility = Visibility.Visible;
		}


		private void MouseLeave_TornaCirculoInvisivel(object sender, System.Windows.Input.MouseEventArgs e) {
			AreaZoom.Visibility = Visibility.Hidden;
		}

		private void cliqueDoMouse(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            AreaZoom.Visibility = Visibility.Visible;

            double x = e.GetPosition(AreaEventos).X;
            double y = e.GetPosition(AreaEventos).Y;

            _scale_transform.ScaleX = 4;
            _scale_transform.ScaleY = 4;

			_ponto_clicado = new Point(x,y);
		}

		private void descliqueDoMouse(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{

			
            double x = e.GetPosition(AreaEventos).X;
            double y = e.GetPosition(AreaEventos).Y;
            ListaPontos.Add(new Point(x,y));
            _scale_transform.ScaleX = 1;
            _scale_transform.ScaleY = 1;

            _ponto_clicado = null;
		}

		private void MouseDown_ApagaMarcadores(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
       
           
            double x = e.GetPosition(AreaEventos).X;
            double y = e.GetPosition(AreaEventos).Y;

            double xscale = _scale_transform.CenterX;
            double yscale = _scale_transform.CenterY;

            double a = (x - xscale);

            _ponto_clicado = null;

		}

	}
}