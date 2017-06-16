using System.Drawing;
using Miotec.Vert3d.DomainModel;

namespace TestesManuaisVert3D
{
    class Program
    {

      
        static void Main(string[] args)
        {
                        
            var im = new Bitmap("../../fringe.png");
            var pr = new Projecao();
            var calib = new Calibracao();

            var marc = new Marcadores();
            marc.VP = new PontoEstereometria(405, 409);
            marc.DL = new PontoEstereometria(881, 338);
            marc.DR = new PontoEstereometria(886, 475);
            marc.SP = new PontoEstereometria(888, 415);

            var estereometria = new Estereometria(im,pr,calib,marc);

            estereometria.Executar();     


        }
    }
}
