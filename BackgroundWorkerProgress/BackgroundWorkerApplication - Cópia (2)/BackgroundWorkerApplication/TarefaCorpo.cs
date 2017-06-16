using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace BackgroundWorkerApplication
{
    class TarefaCorpo : SubTarefa
    {

        public TarefaCorpo(BackgroundWorker mestre) : base(mestre) { }

        public override void Executar() {

            ProgressoGeral = 0;            
            
            int etapa = 0;

            PesosEtapas = new List<double>(){
                0.2,
                0.6,
                0.2
            };

            

            var tarefacabeca = new TarefaCabeca(Mestre);
            tarefacabeca.AtualizouProgresso += (sender, e) => {             
                OnAtualizouProgresso(
                    ProgressoGeral + e.Proporcao * PesosEtapas[etapa],
                    "Cabeça");
            };
            tarefacabeca.Executar();

            if (Mestre.CancellationPending) return;

            ProgressoGeral += PesosEtapas[etapa];
            etapa++;



            var tarefatronco = new TarefaTronco(Mestre);
            tarefatronco.AtualizouProgresso += (sender, e) => {
                OnAtualizouProgresso(
                    ProgressoGeral + e.Proporcao * PesosEtapas[etapa],
                    e.Mensagem);
            };
            tarefatronco.Executar();

            if (Mestre.CancellationPending) return;

            ProgressoGeral += PesosEtapas[etapa];
            etapa++;



            var tarefapernas = new TarefaPernas(Mestre);
            tarefapernas.AtualizouProgresso += (sender, e) => {
                OnAtualizouProgresso(
                    ProgressoGeral + e.Proporcao * PesosEtapas[etapa],
                    "Pernas");
            };
            tarefapernas.Executar();

            if (Mestre.CancellationPending) return;

        }

    }
}
