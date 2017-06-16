using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Miotec.Vert3d.DomainModel;
using Miotec.Vert3d.Application.ViewModels;
using System.Drawing;
using Vert3dImagemIndividual;
using Miotec.Vert3d.Application.Model;
using System.IO.Compression;
using System.Xml.Serialization;

namespace EstereometriaEmLoteJanaina
{
    class Program
    {
        static void Main(string[] args)
        {
            // para cada pasta de arquivo:
            foreach(string dir in Directory.GetDirectories(@"C:\Users\helton\Google Drive\00 MIOTEC\02 Vert 3D\Exames Janaína\Exames")) {
                var shortpath = Path.GetFileNameWithoutExtension(dir);
                var pastadestino = shortpath.Split(new string[]{" - "}, StringSplitOptions.None).First();

                Directory.CreateDirectory(pastadestino);
                
                string nomeimagem = Path.Combine(dir, "imagemfranja.png");

                var imagemfranja = new Bitmap(pastadestino + nomeimagem);

                var coletavm = new ColetaViewModel(pastadestino) {
                                        ImagemBranca = imagemfranja,
                                        ImagemFranja = imagemfranja,
                                    };

                var capturavm = new CapturaViewModelFake();
                capturavm.ProjecaoUtilizada.getImagem(1,0);
                var projecao = capturavm.ProjecaoUtilizada;

                Exame exame;
                string examefilename = Directory.GetFiles(dir).Single(fn => fn.EndsWith(".xml.gz"));
                FileStream fs = new FileStream(examefilename,  FileMode.Open);
                using (var gz = new GZipStream(fs, CompressionMode.Decompress)) {
                    var serializer = new XmlSerializer(typeof(Exame));
                    exame = (Exame)serializer.Deserialize(gz);                    
                }


                // iniciar uma estereometria
                var estereometria = new Estereometria(
                    imagemfranja,
                    projecao, 
                    new Calibracao(), 
                    , 
                    pastadestino);

            }
        }
    }
}
