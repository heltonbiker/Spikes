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
using System.Windows.Xps.Packaging;

namespace XPStoPDF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
		}

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var dc = (ViewModel)(this.DataContext);
            var doc = dc.Relatorio;

            string xpspath = "c:/Miotec/Vert3d/temp/relatorio.xps";
            var xpsdoc = new XpsDocument(xpspath, System.IO.FileAccess.Write);
            var xpsdocwriter = XpsDocument.CreateXpsDocumentWriter(xpsdoc);
            xpsdocwriter.Write(doc);
            xpsdoc.Close();
            PdfSharp.Xps.XpsConverter.Convert(xpspath, "c:/Miotec/Vert3d/temp/relatorio.pdf", 0);
        }
	}
}