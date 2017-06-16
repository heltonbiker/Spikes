using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace Esferinha3D_DataBinding
{
    public class Esferinha {

        const double raio = 5;
        
        public MeshGeometry3D Geometria {
            get {

                int vsegs = 10;
                int usegs = vsegs * 2;
                
                //double ustep = ( 2 * Math.PI ) / (usegs + 1);
                //double vstep = ( 2 * Math.PI ) / (vsegs + 1);

                Point3D[,] nodes = new Point3D[usegs+1, vsegs+1];

                for (int u = 0; u <= usegs; u++) {     // meridianos
                    for (int v = 0; v <= vsegs; v++) { // paralelos
                        double U = u * Math.PI * 2 / usegs;
                        double V = v * Math.PI / vsegs - Math.PI * 0.5; // * Math.PI * 2 / vsegs - Math.PI * 0.5;
                        double x = raio * Math.Cos(V) * Math.Sin(U);
                        double y = raio * Math.Cos(V) * Math.Cos(U);
                        double z = raio * Math.Sin(V);

                        nodes[u,v] = new Point3D(x,y,z);
                    }
                }

                var positions = new Point3DCollection();
                var triangle_indices = String.Empty;

                int somador = 0;
                for (int i = 0; i < usegs; i++) {
                    for (int j = 0; j < vsegs; j++) {
                        positions.Add(nodes[i,j]);
                        positions.Add(nodes[i,j+1]);
                        positions.Add(nodes[i+1,j]);
                        positions.Add(nodes[i+1,j+1]);
						
						triangle_indices += String.Format("{2} {0} {1} {1} {3} {2} ",
														somador,
														somador + 1,
														somador + 2,
														somador + 3);
						somador += 4;
                    }

                }


                var resultado = new MeshGeometry3D() {
                    Positions = positions,
                    TriangleIndices = Int32Collection.Parse(triangle_indices)
                };

                return resultado;
            }
        }
       

    }
}
