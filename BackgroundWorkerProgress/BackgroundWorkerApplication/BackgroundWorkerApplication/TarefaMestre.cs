using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace BackgroundWorkerApplication
{
    class TarefaPrincipal : SubTarefa
    {

        public TarefaPrincipal(BackgroundWorker mestre) : base(mestre) { }

        public void Executar() {


            var tarefaum = new TarefaUm(Mestre);
            var duracaotarefaum = 0.4;
            tarefaum.AtualizouProgresso += (sender, e) => {
                Mestre.ReportProgress((int)(e.Porcentagem * duracaotarefaum), "Executando Tarefa Um...");
            };
            tarefaum.Executar();

            if (Mestre.CancellationPending) {
                return;
            }

            var tarefadois = new TarefaDois(Mestre);
            var duracaotarefadois = 0.6;
            tarefadois.AtualizouProgresso += (sender, e) => {
               Mestre.ReportProgress((int)(100 * (e.Proporcao * duracaotarefadois + duracaotarefaum)), "Executando Tarefa Dois...");
            };
            tarefadois.Executar();

        }

    }
}
