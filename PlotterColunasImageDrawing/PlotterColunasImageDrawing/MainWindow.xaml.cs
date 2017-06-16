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
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace PlotterColunasImageDrawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        
        List<double> sinal;
        
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            sinal = LerArquivo(@"C:\Miotec\Temp\out.txt");

            SizeChanged += new SizeChangedEventHandler((a,b) => {NotifyPropertyChanged("grafico");});
        }

        public GeometryDrawing grafico
        {
            get
            {
                var resultado = new GeometryDrawing(){
                        Pen = new Pen(Brushes.Black, 1)
                    };

                StreamGeometry geometry = new StreamGeometry();
                geometry.Transform = new TranslateTransform(0.5,0.5);

                using (StreamGeometryContext cr = geometry.Open())
                {                                        
                    var r = new Random();
                    var scale = 30;
                    for (int n = 1; n < window.ActualWidth; n++)
                    {
                        cr.BeginFigure(new Point(n,0), false, false);
                        cr.LineTo(new Point(n, r.NextDouble()*2*scale-scale), true, false);
                    }
                }
                geometry.Freeze();
                resultado.Geometry = geometry;
                return resultado;
            }
        }

        List<double> LerArquivo (String path)
        {
            var resultado = File.ReadAllLines(path)
                                .Select(l => Double.Parse(l))
                                .ToList();            
            return resultado;
        }




        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
