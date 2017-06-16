using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace DrawingGraficos
{
    public class EscalaConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Serve para converter unidades do modelo em unidades pixels

            // valor em pixels, valor em unidades do modelo
            double larguracontainer = (double)values[0];
            double alturacontainer = (double)values[1];
            Rect limites = (Rect)values[2];
            double larguraobjeto = limites.Width;
            double alturaobjeto = limites.Height;

            double result = Math.Min(larguracontainer/larguraobjeto,
                                     alturacontainer/alturaobjeto);

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class TranslacaoConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double dimensaopainel = (double)values[0];
            double dimensaoobjeto = (double)values[1];
            double offsetobjeto = (double)values[2];

            return offsetobjeto*(dimensaopainel/dimensaoobjeto);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class ViewportConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // container, limits       
            var larguracontainer = (double)values[0];
            var alturacontainer = (double)values[1];
            var limits = (Rect)values[2];

            var escalax = larguracontainer / limits.Width;
            var escalay = alturacontainer / limits.Height;
            var escalaf = Math.Min(escalax, escalay);

            var escala = new ScaleTransform(escalaf, -escalaf);

            var translatex = (limits.Left + limits.Width * 0.5) * -escalaf;
            var translatey = (limits.Bottom - limits.Height * 0.5) * escalaf;

            var translacao = new TranslateTransform(translatex, translatey);


            var resultado = new TransformGroup();
            resultado.Children.Add(escala);
            resultado.Children.Add(translacao);

            return resultado;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }


    public class MultiplicacaoConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //um número, outro número
            if (!values.Any(p => (p != null) || (p != DependencyProperty.UnsetValue))) {
                return (double)values[0] * (double)values[1];
            } else return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
