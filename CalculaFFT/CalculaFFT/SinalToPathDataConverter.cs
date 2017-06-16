using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace CalculaFFT
{
	class SinalToPathDataConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var amostras = value as IEnumerable<double>;

			if (amostras == null)
				return null;

			var sb = new StringBuilder("M");
			int x_coord = 0;
			foreach (var y_coord in amostras)
			{
				var s = string.Format(CultureInfo.InvariantCulture, " {0} {1}", x_coord++, y_coord);
				sb.Append(s);
			}

			var result = Geometry.Parse(sb.ToString());

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
