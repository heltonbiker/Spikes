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
using Miotec.Vert3d.Application.ViewModels;
using Miotec.Vert3d.Application.Model;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Vert3dImagemIndividual
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		
        Exame exame;
        ColetaViewModel coletavm;
        
        public MainWindow()
		{

            this.InitializeComponent();


            string caminhoexame = @"C:\Miotec\Vert3d\Exames\2014-05-28 15-14-22 - c9461437-3480-47f7-989f-f7885cd93545 - cd80b88b-f3c5-4389-9040-128f3af5cc8c";
            string pasta = System.IO.Path.GetFileName(caminhoexame) + System.IO.Path.DirectorySeparatorChar;

            Directory.CreateDirectory(pasta);

            var imagembranca = new Bitmap(System.IO.Path.Combine(caminhoexame, "imagembranca.png"));
            var imagemfranja = new Bitmap(System.IO.Path.Combine(caminhoexame, "imagemfranja.png"));

            coletavm = new ColetaViewModel(pasta) {
                                    ImagemBranca = imagembranca,
                                    ImagemFranja = imagemfranja,
                                };

            var capturavm = new CapturaViewModelFake();
            capturavm.ProjecaoUtilizada.getImagem(1,0);
            
            coletavm.CapturaVM = capturavm;

            this.DataContext = coletavm;                

		}


        private Bitmap abreImagem(string caminho) {
            return new Bitmap(caminho);
        }
	}
}