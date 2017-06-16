using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Sphere3D
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
/*
			MeshGeometry3D mesh1 = (MeshGeometry3D)Grid1.Resources["MeshGeometry3D1"];

			double r = 20;
			int n = 10;

			mesh1.Positions.Add(new Point3D(0, 0, 0));
			for (int dividerX = 0; dividerX < (4*n+4); dividerX++)
			{
				double alpha = Math.PI / 180.0 * 90.0 / (n + 1) * dividerX;
				double x = r * Math.Cos(alpha);
				double z = -1 * r * Math.Sin(alpha);

				mesh1.Positions.Add(new Point3D(x, 0, z));
				mesh1.TriangleIndices.Add(0);
				mesh1.TriangleIndices.Add(dividerX + 1);
				mesh1.TriangleIndices.Add((dividerX==(4*n+3))?1:(dividerX + 2));
			}
 */
		}

	}
}