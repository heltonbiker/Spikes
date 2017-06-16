using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace PlotterWriteableBitmap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public WriteableBitmap writeableBitmap;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }


        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            writeableBitmap = new WriteableBitmap((int)w.ActualWidth, (int)w.ActualHeight,
                                                   96, 96, PixelFormats.Rgb24, null
            );

            ImagemGrafico.Source = writeableBitmap;
        }



        void DrawPixel(MouseEventArgs e)
        {
            int column = (int)e.GetPosition(ImagemGrafico).X;
            int row = (int)e.GetPosition(ImagemGrafico).Y;


            byte[] ColorData = { 255, 40, 0 };

            Int32Rect rect = new Int32Rect(
                    (int)(e.GetPosition(ImagemGrafico).X),
                    (int)(e.GetPosition(ImagemGrafico).Y),
                    1,
                    1);

            writeableBitmap.Lock();
            writeableBitmap.WritePixels(rect, ColorData, 4, 0);
            writeableBitmap.AddDirtyRect(rect);
            writeableBitmap.Unlock();
        }



    }
}
