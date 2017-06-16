using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using System;

namespace IntegracaoColetaVVM.View
{
	public partial class MarcadoresView : UserControl
	{
		
        EllipseGeometry _geometria_circulo;
        ScaleTransform _scale_transform;

        //public double Escala { get { return _escala; } }
        double _escala;

        //public ObservableCollection<Point> ListaPontos { get; set; }
    
        Point? _ponto_clicado_norm;

        // CONSTRUTOR
        public MarcadoresView()
		{
			this.InitializeComponent();

			//ListaPontos = new ObservableCollection<Point>();
			
            _geometria_circulo = (EllipseGeometry)this.Resources["geometriacirculo"];
            _scale_transform = (ScaleTransform)this.Resources["transform"];
            _escala = (double)this.Resources["escala"];

            GridZoom.Visibility = Visibility.Hidden;
            CirculoGuia.Visibility = Visibility.Hidden;
		}




        private void AreaEventos_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
        	CirculoGuia.Visibility = Visibility.Visible;
        }


        private void AreaEventos_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
        	CirculoGuia.Visibility = Visibility.Hidden;
        }


        private void AreaEventos_MouseMove(object sender, System.Windows.Input.MouseEventArgs e) {

            double x = e.GetPosition(AreaEventos).X;
            double y = e.GetPosition(AreaEventos).Y;


        	if (_ponto_clicado_norm == null) {

                _geometria_circulo.Center = new Point(x, y);

                _scale_transform.CenterX = x;
                _scale_transform.CenterY = y;
            }

        }


        private void AreaEventos_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {

            double x = e.GetPosition(AreaEventos).X;
            double y = e.GetPosition(AreaEventos).Y;

            double w = ImagemOriginalMarcadores.ActualWidth;
            double h = ImagemOriginalMarcadores.ActualHeight;

            _ponto_clicado_norm = new Point(x/w, y/h);

            GridZoom.Visibility = Visibility.Visible;

            CirculoGuia.RenderTransform = new ScaleTransform(_escala, _escala, x, y);

        }

        private void AreaEventos_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            double x = e.GetPosition(AreaEventos).X;
            double y = e.GetPosition(AreaEventos).Y;

            double w = ImagemOriginalMarcadores.ActualWidth;
            double h = ImagemOriginalMarcadores.ActualHeight;

            double xnorm = x/w;
            double ynorm = y/h;

            double deltax = xnorm - _ponto_clicado_norm.Value.X;
            double deltay = ynorm - _ponto_clicado_norm.Value.Y;

            double newx = _ponto_clicado_norm.Value.X + deltax/_escala;
            double newy = _ponto_clicado_norm.Value.Y + deltay/_escala;

            var lst = (ObservableCollection<System.Windows.Point>)MarcadoresSelecionados.ItemsSource;
            lst.Add(new System.Windows.Point(newx, newy));

            GridZoom.Visibility = Visibility.Hidden;

            _ponto_clicado_norm = null;

            CirculoGuia.RenderTransform = null;

        }

        private void AreaEventos_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            double x = e.GetPosition(AreaEventos).X;
            double y = e.GetPosition(AreaEventos).Y;

            double w = ImagemOriginalMarcadores.ActualWidth;
            double h = ImagemOriginalMarcadores.ActualHeight;

            var lst = (ObservableCollection<System.Windows.Point>)MarcadoresSelecionados.ItemsSource;

            int p = 0;
            while (p < lst.Count) {

                Point ponto = lst[p];
 
                double px = ponto.X * w;
                double py = ponto.Y * h;

                double dx = px - x;
                double dy = py - y;

                double raio = _geometria_circulo.RadiusX;

                if (Math.Sqrt(dx*dx + dy*dy) < raio)
                    lst.Remove(ponto);
                else
                    p++;
            }

        }

	}



	
}