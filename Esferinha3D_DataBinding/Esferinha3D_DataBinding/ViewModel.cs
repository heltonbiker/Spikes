using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace Esferinha3D_DataBinding
{
    public class ViewModel
    {

        public ViewModel() {}
        
        public Esferinha esferinha {
            get {
                return new Esferinha();
            }
        }
		
		public Point3DCollection ListaPontos {
			get {
				return new Point3DCollection() {
                    new Point3D (0,0,20),
                    new Point3D (20,0,0),
                    new Point3D (20,20,0)
                };
			}
		}

    }
}
