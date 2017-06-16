using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace BackgroundWorkerApplication
{
    class TarefaDois : SubTarefa
    {

        public TarefaDois(BackgroundWorker mestre) : base(mestre) {}

        public void Executar() {
            
            var tarefadoisum = new TarefaUm(Mestre);
            tarefadoisum.AtualizouProgresso += (sender, e) => {
                this.OnAtualizouProgresso(e.Parcial, e.Total);
            };



            if (Mestre.CancellationPending) return;




            var tarefadoisdois = new TarefaUm(Mestre);

            tarefadoisdois.AtualizouProgresso += (sender, e) => {
                this.OnAtualizouProgresso(e.Parcial, e.Total);
            };

            tarefadoisdois.Executar();

        }

    }
}
