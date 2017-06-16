using System;
using System.Collections.Generic;
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
using AForge.Video.DirectShow;
using AForge.Video;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;

namespace GerenciadorDeColeta
{
	/// <summary>
	/// Interaction logic for VisualizacaoCamera.xaml
	/// </summary>
	public partial class VisualizacaoCamera : UserControl
	{
		public VisualizacaoCamera()
		{
			this.InitializeComponent();
            this.Loaded +=new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            
            foreach (var capability in videoSource.VideoCapabilities) {
                Console.WriteLine(capability.FrameRate);
            }

            //videoSource.DesiredFrameRate = 30;
            videoSource.Start( );
        }

        private void video_NewFrame (object sender, NewFrameEventArgs eventArgs) {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();

            using(MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Bmp);
                ms.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
                
                bitmapImage.Freeze();

                Dispatcher.BeginInvoke(new ThreadStart(delegate {
                    imagem_camera.Source = bitmapImage;
                }));
            }


        }
	}
}