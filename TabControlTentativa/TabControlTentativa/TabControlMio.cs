using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TabControlTentativa
{
	public class TabItemMio : TabItem
	{
		public TabItemMio()
		{

		}

		public Geometry Icone
		{
			get { return (Geometry)GetValue(IconeProperty); }
			set { SetValue(IconeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Icone.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IconeProperty =
			DependencyProperty.Register("Icone", typeof(Geometry), typeof(TabItemMio), new PropertyMetadata(default(Geometry)));

	}
}
