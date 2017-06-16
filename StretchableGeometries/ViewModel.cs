using GalaSoft.MvvmLight;
using Miotec.ModeloDomínio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace StretchableGeometries
{
	public class ViewModel : ViewModelBase
	{

		public Intervalo IntervaloVertical
		{
			get { return _intervalo_vertical; }
			set
			{
				_intervalo_vertical = value;
				RaisePropertyChanged(() => IntervaloVertical);
			}
		}
		Intervalo _intervalo_vertical;


		public Intervalo IntervaloHorizontal
		{
			get { return _intervalo_horizontal; }
			set
			{
				_intervalo_horizontal = value;
				RaisePropertyChanged(() => IntervaloHorizontal);
			}
		}
		Intervalo _intervalo_horizontal;


		Geometry circulo = new EllipseGeometry(new Point(0.5, 0.5), 0.4, 0.4);
	}
}
