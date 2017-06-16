using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FluxoDeTelas
{
    public class ShellViewModel : ViewModelBase {

        TelaPacientes _tela_pacientes;
        TelaColeta _tela_coleta;

        public ViewModelBase TelaAtiva {
            get { return _tela_ativa; }
            set {
                _tela_ativa = value;
                RaisePropertyChanged(() => TelaAtiva);
            }
        }
        ViewModelBase _tela_ativa;


        // CONSTRUTOR
        public ShellViewModel () {
            _tela_pacientes = new TelaPacientes();
            _tela_coleta = new TelaColeta();

            TelaAtiva = _tela_pacientes;
            
        }


        /// <summary>
        /// Cria um novo exame associado ao paciente selecionado.
        /// </summary>
        public void CriarExame () {
           TelaAtiva = _tela_coleta; 
        }
        private bool PodeCriarExame()
        {            
            return true;
        }
        RelayCommand _comando_criar_exame;
        public ICommand ComandoCriarExame {
            get {
                if (_comando_criar_exame == null) {
                    _comando_criar_exame = new RelayCommand(param => CriarExame(),
                                                    param => PodeCriarExame());
                }
                return _comando_criar_exame;
            }
        }        



        public void Cancelar () { 
            TelaAtiva = _tela_pacientes;
        }
        private bool PodeCancelar()
        {
            if (true) // <-- incluir teste aqui!!!
                return true;
            return false;
        }
        RelayCommand _comando_cancelar;
        public ICommand ComandoCancelar {
            get {
                if (_comando_cancelar == null) {
                    _comando_cancelar = new RelayCommand(param => Cancelar(),
                                                    param => PodeCancelar());
                }
                return _comando_cancelar;
            }
        }


    }
}
