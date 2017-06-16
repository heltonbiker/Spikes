using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ButtonHighlights
{
	public static class PropriedadeAnexada
	{
		public static readonly DependencyProperty CorProperty
			= DependencyProperty.RegisterAttached(
				"Cor",
				typeof(Color),
				typeof(PropriedadeAnexada),
				new PropertyMetadata(default(Color))
			);

		public static Color GetCor(DependencyObject element)
		{
			return (Color)element.GetValue(CorProperty);
		}

		public static void SetCor(DependencyObject element, Color value)
		{
			element.SetValue(CorProperty, value);
		}
	}
}
