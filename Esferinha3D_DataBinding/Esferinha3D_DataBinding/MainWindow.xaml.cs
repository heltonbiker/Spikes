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
using System.Windows.Media.Media3D;

namespace Esferinha3D_DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var dc = this.DataContext as ViewModel;

            foreach (var p in dc.ListaPontos) {
                var bolinha = new GeometryModel3D() {
                    Geometry = new Esferinha().Geometria,
                    Material = new DiffuseMaterial(Brushes.Silver),
                    Transform = new TranslateTransform3D((Vector3D)p)
                };
                grupobolinhas.Children.Add(bolinha);
            }
        }
    }
}
