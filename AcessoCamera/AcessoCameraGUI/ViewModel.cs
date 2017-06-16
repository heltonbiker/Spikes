using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using AcessoCamera;
using System.Windows.Input;
using System.IO;
using System.Drawing;
using System.Windows;



namespace AcessoCameraGUI
{
    public class ViewModel : ViewModelBase
    {

        public Bitmap FrameCamera
        {
            get { return _frame_camera; }
            set
            {
                _frame_camera = value;
                RaisePropertyChanged(() => FrameCamera);
            }
        }
        Bitmap _frame_camera;


        public Bitmap ImagemBranca
        {
            get { return _imagem_branca; }
            set
            {
                _imagem_branca = value;
                RaisePropertyChanged(() => ImagemBranca);
            }
        }
        Bitmap _imagem_branca;


        public Bitmap ImagemFranja
        {
            get { return _imagem_franja; }
            set
            {
                _imagem_franja = value;
                RaisePropertyChanged(() => ImagemFranja);
            }
        }
        Bitmap _imagem_franja;

        CameraService _camera_service;

        //  CONSTRUTOR
        public ViewModel()
        {
            this._camera_service = new CameraService();
            _camera_service.NovoFrame += new NovoFrameEventHandler(_camera_service_NovoFrame);
            _camera_service.Start();

        }




        public void CapturarFrame()
        {
            ImagemBranca = FrameCamera;
        }
        public bool PodeCapturarFrame()
        {
            if (_camera_service != null && _camera_service.isRunning())
            {
                return true;
            }
            return false;
        }
        RelayCommand _comando_capturar_frame;
        public ICommand ComandoCapturarFrame
        {
            get
            {
                if (_comando_capturar_frame == null)
                {
                    _comando_capturar_frame = new RelayCommand(param => CapturarFrame(),
                                                                   param => PodeCapturarFrame());
                }
                return _comando_capturar_frame;
            }
        }


        public void ConfirmarCaptura()
        {
            SalvarImagem(ImagemBranca);
        }
        public bool PodeConfirmarCaptura()
        {
            if (ImagemBranca != null)
            {
                return true;
            }

            return false;
        }
        RelayCommand _comando_confirmar_captura;
        public ICommand ComandoConfirmarCaptura
        {
            get
            {
                if (_comando_confirmar_captura == null)
                {
                    _comando_confirmar_captura = new RelayCommand(param => ConfirmarCaptura(), param => PodeConfirmarCaptura());
                }
                return _comando_confirmar_captura;
            }
        }


        public void CancelarCaptura()
        {
            _camera_service.Stop();
            Environment.Exit(0);
            
        }
        public bool PodeCancelarCaptura()
        {
            return true;
        }
        RelayCommand _comando_cancelar_captura;
        public ICommand ComandoCancelarCaptura
        {
            get
            {
                if (_comando_cancelar_captura == null)
                {
                    _comando_cancelar_captura = new RelayCommand(param => CancelarCaptura(), param => PodeCancelarCaptura());
                }
                return _comando_cancelar_captura;
            }
        }

        void _camera_service_NovoFrame(object sender, NovoFrameArgs e)
        {
            FrameCamera = e.Frame;
        }

        private void SalvarImagem(Bitmap imagem)
        {
            imagem.Save("Imagem capturada.png");
        }

        
    }
}
