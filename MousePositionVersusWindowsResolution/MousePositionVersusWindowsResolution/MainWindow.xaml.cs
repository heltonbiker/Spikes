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

namespace MousePositionVersusWindowsResolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }



        private void MouseEventArea_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point p = e.GetPosition(sender as FrameworkElement);

            Size s = MouseEventArea.RenderSize;

            Double x = p.X;
            Double y = p.Y;
            Double w = s.Width;
            Double h = s.Height;

            X.Text = x.ToString("N2");
            Y.Text = y.ToString("N2");

            xnorm.Text = (x / w).ToString("N2");
            ynorm.Text = (y / h).ToString("N2");
        }



    }
}
