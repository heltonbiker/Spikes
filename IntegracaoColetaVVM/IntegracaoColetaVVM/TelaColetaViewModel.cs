using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using IntegracaoColetaVVM.InfraEstrutura;
using IntegracaoColetaVVM.View;
using IntegracaoColetaVVM.Model;
using IntegracaoColetaVVM.Dominio;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Timers;
using System.Collections.ObjectModel;

namespace IntegracaoColetaVVM
{
    public class TelaColetaViewModel : ViewModelBase {


        ProjetorService _projetor_service;
        CameraService2 _camera_service;

        CapturaView _captura;
        MarcadoresView _marcadores;

        public Control ProcedimentoAtivo {
            get { return _procedimento_ativo; }
            set {
                _procedimento_ativo = value;
                RaisePropertyChanged(() => ProcedimentoAtivo);
            }
        }
        Control _procedimento_ativo;


        public ObservableCollection<System.Windows.Point> MarcadoresClicados { get; set; }


        public double MinPosition {
            get { return _min_position; }
            set {
                _min_position = value;
                RaisePropertyChanged(() => MinPosition);
                if (_projetor_service != null)
                    _projetor_service.MinValue = _min_position;
            }
        }
        double _min_position;

        public double MaxPosition {
            get { return _max_position; }
            set {
                _max_position = value;
                RaisePropertyChanged(() => MaxPosition);
                if (_projetor_service != null)
                    _projetor_service.MaxValue = _max_position;
            }
        }
        double _max_position;



        public Bitmap  UltimoFrameCamera {
            get { return _frame_camera; }
            set
            {
                _frame_camera = value;
                RaisePropertyChanged(() => UltimoFrameCamera);
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




        // CONSTRUTOR
        public TelaColetaViewModel() {

            _projetor_service = new ProjetorService();
            _camera_service = new CameraService2();
            _camera_service.NovoFrame += new NovoFrameEventHandler(_camera_service_NovoFrame);

            MarcadoresClicados = new ObservableCollection<System.Windows.Point>();

            MinPosition = 0;
            MaxPosition = 1;

            _captura = new CapturaView();
            _captura.DataContext = this;

            _marcadores = new MarcadoresView();
            _marcadores.DataContext = this;

            ProcedimentoAtivo = _captura;

            IniciarColeta();

        }

        void _camera_service_NovoFrame(object sender, NovoFrameArgs e)
        {
            UltimoFrameCamera = e.Frame;
        }



        public void IniciarColeta () { 
            if (!_projetor_service.IsRunning()) {
                _projetor_service.Start();
            }
            if (!_camera_service.isRunning()) {
                _camera_service.Start();
            }
        }
        private bool PodeIniciarColeta()
        {
            if (true) // <-- incluir teste aqui!!!
                return true;
            return false;
        }
        RelayCommand _comando_iniciar_coleta;
        public ICommand ComandoIniciarColeta {
            get {
                if (_comando_iniciar_coleta == null) {
                    _comando_iniciar_coleta = new RelayCommand(param => IniciarColeta(),
                                                    param => PodeIniciarColeta());
                }
                return _comando_iniciar_coleta;
            }
        }


        public void IrParaTelaCaptura () { 
            ProcedimentoAtivo = _captura;
        }
        private bool PodeIrParaTelaCaptura()
        {
            if (true) // <-- incluir teste aqui!!!
                return true;
            return false;
        }
        RelayCommand _comando_ir_para_tela_captura;
        public ICommand ComandoIrParaTelaCaptura {
            get {
                if (_comando_ir_para_tela_captura == null) {
                    _comando_ir_para_tela_captura = new RelayCommand(param => IrParaTelaCaptura(),
                                                    param => PodeIrParaTelaCaptura());
                }
                return _comando_ir_para_tela_captura;
            }
        }


        public void IrParaTelaMarcadores () { 
            ProcedimentoAtivo = _marcadores;
        }
        private bool PodeIrParaTelaMarcadores()
        {
            if (true) // <-- incluir teste aqui!!!
                return true;
            return false;
        }
        RelayCommand _comando_ir_para_tela_marcadores;
        public ICommand ComandoIrParaTelaMarcadores {
            get {
                if (_comando_ir_para_tela_marcadores == null) {
                    _comando_ir_para_tela_marcadores = new RelayCommand(param => IrParaTelaMarcadores(),
                                                    param => PodeIrParaTelaMarcadores());
                }
                return _comando_ir_para_tela_marcadores;
            }
        }


        public void ConfirmarCaptura()
        {
            SalvarImagem(ImagemBranca, "Branca");
            SalvarImagem(ImagemFranja, "Franja");
            //_camera_service.Stop();
            ProcedimentoAtivo = _marcadores;
        }
        public bool PodeConfirmarCaptura() {
            if (ImagemFranja != null)
                return true;
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


        public void CancelarCaptura() {
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


        public void CapturarFrame()
        {
            ImagemFranja = UltimoFrameCamera;

            _projetor_service.ProjetaBranca();

            Timer t = new Timer(1000);
            t.AutoReset = false;
            t.Elapsed += new ElapsedEventHandler(CapturaImagemBranca);
            t.Start();           
        }
        public bool PodeCapturarFrame()
        {
            if (_camera_service != null && UltimoFrameCamera != null)
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

        void CapturaImagemBranca(object sender, ElapsedEventArgs e) {

            ImagemBranca = UltimoFrameCamera;
            
            Timer t = new Timer(500);
            t.AutoReset = false;
            t.Elapsed+=new ElapsedEventHandler(ProjetaFranjaDeNovo);
            t.Start();                        
        }

        void ProjetaFranjaDeNovo(object sender, ElapsedEventArgs e) {
            _projetor_service.ProjetaFranja();        
        }



        public void ConcluirMarcadores () {  }
        private bool PodeConcluirMarcadores()
        {
            if (MarcadoresClicados.Count == 4)
                return true;
            return false;
        }
        RelayCommand _comando_concluir_marcadores;
        public ICommand ComandoConcluirMarcadores {
            get {
                if (_comando_concluir_marcadores == null) {
                    _comando_concluir_marcadores = new RelayCommand(param => ConcluirMarcadores(),
                                                    param => PodeConcluirMarcadores());
                }
                return _comando_concluir_marcadores;
            }
        }



        private void SalvarImagem(Bitmap imagem, String nome)
        {
            imagem.Save(nome + ".png");
        }



        public void PegaPosicao(double x, double y) {
            Console.WriteLine(string.Format("{0}, {1}", x, y));
        }

    }
}
