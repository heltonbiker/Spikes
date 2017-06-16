using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace BackgroundWorkerApplication
{
    class TarefaTronco : SubTarefa
    {

        public TarefaTronco(BackgroundWorker mestre) : base(mestre) {}

        public override void Executar() {

            double progressogeral = 0;
            
            double pesoetapa = 0.7;

            var tarefatorax = new TarefaTorax(Mestre);
            tarefatorax.AtualizouProgresso += (sender, e) => {
                OnAtualizouProgresso(
                    progressogeral + e.Proporcao * pesoetapa,
                    "Torax");
            };

            tarefatorax.Executar();

            if (Mestre.CancellationPending) return;

            progressogeral += pesoetapa;



            pesoetapa = 0.3;

            var tarefaabdome = new TarefaAbdome(Mestre);
            tarefaabdome.AtualizouProgresso += (sender, e) => {
                OnAtualizouProgresso(
                    progressogeral + e.Proporcao * pesoetapa,
                    "Tronco");
            };

            tarefaabdome.Executar();

        }


    }
}
