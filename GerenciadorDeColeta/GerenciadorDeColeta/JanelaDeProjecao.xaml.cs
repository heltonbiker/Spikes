using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GerenciadorDeColeta
{
	/// <summary>
	/// Interaction logic for JanelaDeProjecao.xaml
	/// </summary>
	public partial class JanelaDeProjecao : Window
	{

		public JanelaDeProjecao(ProjectionPattern projeção)
		{
			this.InitializeComponent();
            DataContext = projeção;
			
            //this.Loaded +=new RoutedEventHandler(SendToProjectorScreen);
		}


        //void SendToProjectorScreen(object sender, RoutedEventArgs e)
        //{
        //    var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();

        //    var projectorscreen = allScreens.Where(s => s.Bounds.Height == 600).First();

        //    this.WindowState = System.Windows.WindowState.Normal;
        //    this.Left = projectorscreen.Bounds.X + 100;
        //    this.Top = projectorscreen.Bounds.Y + 100;
        //    this.WindowStyle = System.Windows.WindowStyle.None;
        //    this.WindowState = System.Windows.WindowState.Maximized;
        //}
	}
}