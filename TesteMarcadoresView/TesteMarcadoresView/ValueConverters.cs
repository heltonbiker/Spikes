﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace TesteMarcadoresView
{

    [ValueConversion(typeof(System.Drawing.Bitmap), typeof(System.Windows.Media.Imaging.BitmapImage))]
    public class BitmapToSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try {
                Bitmap bmp = (System.Drawing.Bitmap)value;

                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Bmp);
                    ms.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();

                    bitmapImage.Freeze();

                    return bitmapImage;
                }
            }
            catch { return null; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
