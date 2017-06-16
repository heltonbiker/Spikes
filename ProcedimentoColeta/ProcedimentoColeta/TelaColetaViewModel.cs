using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProcedimentoColeta
{
    public class TelaColetaViewModel : ViewModelBase {

        public Control ProcedimentoAtivo {
            get { return _procedimento_ativo; }
            set {
                _procedimento_ativo = value;
                RaisePropertyChanged(() => ProcedimentoAtivo);
            }
        }
        Control _procedimento_ativo;

        Control Captura {
            get {
                if (_captura == null)
                    _captura = new ControleCaptura();
                return _captura;
            }
        }
        Control _captura;

        Control Marcadores {
            get {
                if (_marcadores == null )
                    _marcadores = new ControleIdentificacaoMarcadores();
                return _marcadores;
            }
        }
        Control _marcadores;

        // CONSTRUTOR
        public TelaColetaViewModel() {

            ProcedimentoAtivo = new ControleCaptura();

        }




        public void IrParaTelaCaptura () { 
            ProcedimentoAtivo = Captura;
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
            ProcedimentoAtivo = Marcadores;
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



    }
}
