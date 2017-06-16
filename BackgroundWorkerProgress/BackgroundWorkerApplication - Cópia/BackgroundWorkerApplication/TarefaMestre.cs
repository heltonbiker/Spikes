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
            var duracaotarefaum = 60;
            Medidor.Somador(duracaotarefaum);
            var duracaotarefadois = 40;
            Medidor.Somador(duracaotarefadois);
           

            tarefaum.AtualizouProgresso += (sender, e) => {
                Mestre.ReportProgress((int)(e.Porcentagem*Medidor.DistribuidorTempo(duracaotarefaum)), "Executando Tarefa Um...");
            };
            tarefaum.Executar();

            if (Mestre.CancellationPending) {
                return;
            }

            var tarefadois = new TarefaDois(Mestre);

            tarefadois.AtualizouProgresso += (sender, e) => {
                Mestre.ReportProgress((int)(Medidor.DistribuidorTempo(e.Proporcao * duracaotarefaum + duracaotarefadois) * 150), "Executando Tarefa Dois...");
            };
            tarefadois.Executar();

        }

    }
}
