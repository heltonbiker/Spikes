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
using System.Windows.Shapes;

namespace PanZoomMVVM
{
	public partial class CanaisWindow : Window
	{
		public CanaisWindow()
		{
			this.InitializeComponent();

            Loaded += new RoutedEventHandler(CanaisWindow_Loaded);
		}

        void CanaisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var dc = DataContext as CanaisViewModel;

            gridCanais.Children.Clear();

            foreach (var c in dc.Canais)
            {
                gridCanais.Children.Add(new CanalView() { DataContext = c });
            }
            
        }

	}
}