using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miotec.MVVM;
using System.Windows;
using System.Windows.Media;

namespace EscalaMarcadores
{
    public class ViewModel : ViewModelBase {

        Point superior { get { return new Point(0,40); } }
        Point inferior { get { return new Point(0,-10); } }
        Point direito { get { return new Point(-10,0); } }
        Point esquerdo { get { return new Point(10,0); } }

        public Rect Limites {
            get { return new Rect(new Point(-30,-20),
                                  new Point(30, 55));
            }
        }

        public GeometryGroup GeometriaMarcadores {
            get {
                var resultado = new GeometryGroup();
                resultado.Children.Add(new LineGeometry(superior, inferior));
                resultado.Children.Add(new LineGeometry(esquerdo, direito));

                return resultado;
            }
        }

        public GeometryGroup LinhasMarcadores {
            get {
                var linhasup = new LineGeometry(new Point(Limites.Left, superior.Y),
                                                new Point(Limites.Right, superior.Y));
                var linhainf = new LineGeometry(new Point(Limites.Left, inferior.Y),
                                                new Point(Limites.Right, inferior.Y));

                var resultado = new GeometryGroup();
                resultado.Children.Add(linhasup);
                resultado.Children.Add(linhainf);

                return resultado;
            }
        }

    }
}
