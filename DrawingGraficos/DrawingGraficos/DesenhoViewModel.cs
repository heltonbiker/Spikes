using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miotec.MVVM;
using System.Windows;
using System.Windows.Media;
using OxyPlot;

namespace DrawingGraficos
{
    public class DesenhoViewModel : ViewModelBase {

        public Rect Limites {
            get {
                double esq = -40;
                double dir = 30;
                double fundo = -25;
                double topo = 60;
                return new Rect(new Point(esq,fundo),
                                new Point(dir,topo));
            }
        }

        public PointCollection BoundingBoxCorners {
            get {
                return new PointCollection() {
                    Limites.BottomLeft, Limites.TopLeft, Limites.TopRight, Limites.BottomRight
                };
            }
        }


        public List<Point> LinhaSimetria {
            get {
                return new List<Point> {
                    new Point(-5,5),
                    new Point(-5,-5),
                    new Point(5,-5),
                    new Point(5,5),                    
                    new Point(0,5),                    
                    new Point(2,10),
                    new Point(0,15),
                    new Point(-2,20),
                    new Point(-2,25),
                    new Point(-2,30),
                    new Point(-2,35)
                };
            }
        }

        public Geometry GeometriaLinhaSimetria {
            get {
                var sb = new StringBuilder("M");
                foreach (var p in LinhaSimetria) {
                    sb.AppendFormat(" {0};{1}", p.X, p.Y);
                }
                string result = sb.ToString().Replace(",",".").Replace(";",",");
                return Geometry.Parse(result);
            }
        }

    }
}
