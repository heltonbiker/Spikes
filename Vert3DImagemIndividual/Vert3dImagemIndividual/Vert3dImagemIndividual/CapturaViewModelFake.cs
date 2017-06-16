using Miotec.MVVM;
using Miotec.Vert3d.DomainModel;

namespace Vert3dImagemIndividual
{
    public class CapturaViewModelFake : ViewModelBase, ICaptura
    {

        public Calibracao CalibracaoUtilizada
        {
            get {
                if (_calibracao_utilizada == null)
                    _calibracao_utilizada = new Calibracao() {
                        AlturaPixelsCamera = 1024,
                        LarguraPixelsCamera = 768,
                        DistanciaFocalVirtual = 2800,
                        AnguloVertical = -25,
                        TranslacaoHorizontal = 50,
                        TranslacaoVertical = 1210
                    };
                return _calibracao_utilizada;
            }
        }
        Calibracao _calibracao_utilizada;

        public Projecao ProjecaoUtilizada {
            get {
                if (_projecao_utilizada == null)
                    _projecao_utilizada = new Projecao(800, 600);
                return _projecao_utilizada;
            }
        }
        Projecao _projecao_utilizada;


        public void Inicializar() { throw new System.NotImplementedException(); }

        public double MaxPosition
        {
            get
            {
                return 1;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public double MinPosition
        {
            get
            {
                return 0;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void PararCaptura() { }



        public void ProjetaBranca()
        {
            throw new System.NotImplementedException();
        }

        public void ProjetaFranja()
        {
            throw new System.NotImplementedException();
        }


        public void ReiniciarCaptura() { }


        public System.Drawing.Bitmap UltimoFrameCamera
        {
            get
            {
                return null;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
