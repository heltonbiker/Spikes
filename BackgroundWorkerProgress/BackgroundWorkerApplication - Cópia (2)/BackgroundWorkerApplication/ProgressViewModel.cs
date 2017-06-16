using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading;

namespace BackgroundWorkerApplication
{
    public class ProgressViewModel : ViewModelBase {

        public double Progresso {
            get { return _progresso; }
            set {
                _progresso = value;
                RaisePropertyChanged(() => Progresso);
            }
        }
        double _progresso;

        public string Mensagem {
            get { return _mensagem; }
            set {
                _mensagem = value;
                RaisePropertyChanged(() => Mensagem);
            }
        }
        string _mensagem;


        BackgroundWorker worker;


        // CONSTRUTOR
        public ProgressViewModel() {
            Progresso = 0;
            Mensagem = "Clique para iniciar a atividade.";
        }



        public void IniciarAtividade () { 
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerAsync();
        }
        private bool PodeIniciarAtividade()
        {
            if (worker == null || !worker.IsBusy) // <-- incluir teste aqui!!!
                return true;
            return false;
        }
        RelayCommand _comando_iniciar_atividade;
        public ICommand ComandoIniciarAtividade {
            get {
                if (_comando_iniciar_atividade == null) {
                    _comando_iniciar_atividade = new RelayCommand(param => IniciarAtividade(),
                                                    param => PodeIniciarAtividade());
                }
                return _comando_iniciar_atividade;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e) {
            var bw = (BackgroundWorker)sender;

            var tarefa = new TarefaCorpo(bw);

            tarefa.AtualizouProgresso += (obj, args) => {
                bw.ReportProgress((int)(args.Proporcao * 100), args.Mensagem);
            };

            tarefa.Executar();

            if (bw.CancellationPending) {
                e.Cancel = true;    
            }


            bw.ReportProgress(100, "Processamento Concluído!");
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                Mensagem = e.Error.Message;
            }
            
            if (e.Cancelled) {
                Mensagem = "Procedimento Cancelado";
                Progresso = 0;
            }

            CommandManager.InvalidateRequerySuggested();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e){
            Mensagem = (string)e.UserState;
            Progresso = (double)e.ProgressPercentage / 100.0;
        }



        public void CancelarAtividade () {
            if (worker != null) {
                worker.CancelAsync();
            }
        }
        private bool PodeCancelarAtividade()
        {
            if (worker != null && worker.IsBusy) // <-- incluir teste aqui!!!
                return true;
            return false;
        }
        RelayCommand _comando_cancelar_atividade;
        public ICommand ComandoCancelarAtividade {
            get {
                if (_comando_cancelar_atividade == null) {
                    _comando_cancelar_atividade = new RelayCommand(param => CancelarAtividade(),
                                                    param => PodeCancelarAtividade());
                }
                return _comando_cancelar_atividade;
            }
        }

    }
}
