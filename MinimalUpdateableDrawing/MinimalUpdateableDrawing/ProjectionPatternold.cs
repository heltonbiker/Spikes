using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;

namespace MinimalUpdateableDrawing
{
    class Radius : INotifyPropertyChanged {
        
        int _value = 100;

        public int Value {
            get { return _value; }
            set {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property_name) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
        }
    }


    public class EllipseConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null) return null;

            int radius = (int)value;
            int diameter = radius * 2;
            int h = 600;
            int w = 800;

            using (var bmp = new Bitmap(w, h)) {
                using (var g = Graphics.FromImage(bmp)) {
                    g.FillEllipse(System.Drawing.Brushes.Blue, w/2-radius, h/2-radius, radius*2, radius*2);
                }

                using(MemoryStream ms = new MemoryStream()) {
                    bmp.Save(ms, ImageFormat.Bmp);
                    ms.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
