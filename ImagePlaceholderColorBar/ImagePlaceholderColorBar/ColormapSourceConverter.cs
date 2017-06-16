
using System.Windows.Data;
using System.Windows.Media;
namespace ImagePlaceholderColorBar
{
    public class ColormapSourceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double h = (double)values[0];
            double w = (double)values[1];
            Colormap cmap = (Colormap)values[2];
            
            
            return new DrawingImage();
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
