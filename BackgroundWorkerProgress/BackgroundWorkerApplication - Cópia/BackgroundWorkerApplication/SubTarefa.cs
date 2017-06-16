using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

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




        public event AtualizouProgressoEventHandler AtualizouProgresso;

        protected void OnAtualizouProgresso(int parcial, int total) {
            AtualizouProgressoEventHandler handler = AtualizouProgresso;
            if (handler != null)
                handler(this, new AtualizouProgressoArgs(parcial, total));
        }

    }



    public delegate void AtualizouProgressoEventHandler (object sender, AtualizouProgressoArgs e);

    public class AtualizouProgressoArgs : EventArgs {
        
        public int Parcial { get; private set; }
        public int Total { get; private set; }

        public double Proporcao { get { return (double)Parcial / Total; } }

        public int Porcentagem { get { return (int)(100 * Proporcao); } }

        public string Mensagem { get; private set; }

        public AtualizouProgressoArgs(int parcial, int total) {
            Parcial = parcial;
            Total = total;
        }

        public AtualizouProgressoArgs(int parcial, int total, string mensagem) {
            Parcial = parcial;
            Total = total;
            Mensagem = mensagem;
        }
    }

}
