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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace TesteCameraTIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        
        public BitmapSource UltimoFrameCamera
        {
            get { return _ultimoFrameCamera; }
            set
            {
                _ultimoFrameCamera = value;
                RaisePropertyChanged(null);
            }
        }
        BitmapSource _ultimoFrameCamera;
        


        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }


        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var cameraService = new CameraServiceTIS();
            cameraService.NovoFrame += new NovoFrameEventHandler(cameraService_NovoFrame);
            cameraService.Start();
        }

        void cameraService_NovoFrame(object sender, NovoFrameArgs e)
        {
            Bitmap bmp = e.Frame;
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

                UltimoFrameCamera = bitmapImage;
            }
        }








        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
