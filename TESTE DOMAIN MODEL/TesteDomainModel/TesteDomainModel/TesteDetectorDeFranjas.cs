using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miotec.Vert3d.DomainModel;
using System.Drawing;
using System.IO;

namespace TesteDomainModel
{
    [TestClass]
    public class TesteDetectorDeFranjas
    {

        private String path;

        public List<String> FileNames
        {
            get { return Directory.GetFiles("C:/Users/pc1/Dropbox/PequenosProjetos/TESTE DOMAIN MODEL/TesteDomainModel/IMAGENS_FRANJA").ToList(); }
        }

        public String DirectoryPath
        {
            get { return path; }
            set { path = value; }
        }

        [TestMethod()]
        public void TestaTudo()
        {
            foreach (var filename in FileNames)
            {
                var fileinfo = new FileInfo(filename);
                var imagem = new Bitmap(filename);
                TestaSeAListaDeFranjasNaoENull(imagem,fileinfo.Name);
               // TestaSeAListaDeFranjasNaoEstaVazia(imagem,fileinfo.Name);
                TestaSeOIndiceFranjaPrincipalNaoENull(imagem,fileinfo.Name);

            }

        }

        public void TestaSeAListaDeFranjasNaoENull(Bitmap imagem, String filename)
        {
                DetectorFranjas detect = new DetectorFranjas(imagem, imagem.Width / 2);

                detect.Executar();
                NUnit.Framework.Assert.IsNotNull(detect.ListaFranjas);
                System.Diagnostics.Debug.WriteLine(filename + ": " +detect.ListaFranjas.Count);
        }


        public void TestaSeAListaDeFranjasNaoEstaVazia(Bitmap imagem, String filename)
        {
            DetectorFranjas detect = new DetectorFranjas(imagem, imagem.Width / 2);

            detect.Executar();
            NUnit.Framework.Assert.IsNotEmpty(detect.ListaFranjas);
        }


        public void TestaSeOIndiceFranjaPrincipalNaoENull(Bitmap imagem, String filename)
        {
            
            DetectorFranjas detect = new DetectorFranjas(imagem, imagem.Width / 2);

            detect.Executar();
            NUnit.Framework.Assert.IsNotNull(detect.IndiceFranjaPrincipal);

        }

      
        public void TestaSeDaErroComUmaImagemBranca()
        {
            DirectoryPath = "C:/Users/pc1/Dropbox/PequenosProjetos/TESTE DOMAIN MODEL/TesteDomainModel/Imagens2/bandeira-branca.png";
            Bitmap original = new Bitmap(DirectoryPath);
            DetectorFranjas detect = new DetectorFranjas(original, original.Width / 2);

            detect.Executar();
            NUnit.Framework.Assert.IsNotEmpty(detect.ListaFranjas);
            NUnit.Framework.Assert.IsNotNull(detect.ListaFranjas);

            Console.WriteLine(detect.ListaFranjas.Count);            
        }

    }
}
