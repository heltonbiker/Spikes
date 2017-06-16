using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace BackgroundWorkerApplication
{
    
    /// <summary>
    /// Tarefa que ocorre de forma subordinada dentro
    /// do médodo "DoWork" de alguma instância de <see cref="BackgroundWorker"/>.
    /// </summary>
    public class SubTarefa {

        public BackgroundWorker Mestre { get; set; }

        public SubTarefa (BackgroundWorker mestre = null) {
            Mestre = mestre;
        }


        public double ProgressoGeral { get; set; }
        public List<double> PesosEtapas { get; set; }

        public virtual void Executar() {
            int total = new Random().Next(10, 20);
            for (int parcial = 1; parcial < total; parcial++) {
                Thread.Sleep(200);
                OnAtualizouProgresso((double)parcial/total);
                if (Mestre.CancellationPending) {
                    return;
                }
            }

        }





        public event AtualizouProgressoEventHandler AtualizouProgresso;

        protected void OnAtualizouProgresso(double prop, string mensagem) {
            AtualizouProgressoEventHandler handler = AtualizouProgresso;
            if (handler != null)
                handler(this, new AtualizouProgressoArgs(prop, mensagem));
        }

        protected void OnAtualizouProgresso(double prop) {
            AtualizouProgressoEventHandler handler = AtualizouProgresso;
            if (handler != null)
                handler(this, new AtualizouProgressoArgs(prop, null));
        }


    }



    public delegate void AtualizouProgressoEventHandler (object sender, AtualizouProgressoArgs e);

    public class AtualizouProgressoArgs : EventArgs {
        
        public double Proporcao { get; private set; }

        public string Mensagem { get; private set; }

        public AtualizouProgressoArgs(double prop) {
            Proporcao = prop;
        }

        public AtualizouProgressoArgs(double prop, string mensagem) {
            Proporcao = prop;
            Mensagem = mensagem;
        }
    }

}
