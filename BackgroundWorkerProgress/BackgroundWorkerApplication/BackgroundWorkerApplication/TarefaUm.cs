using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace BackgroundWorkerApplication
{
    class TarefaUm : SubTarefa {

        public TarefaUm(BackgroundWorker mestre) : base(mestre) {}

        public void Executar() {
            int total = new Random().Next(100, 200);
            for (int parcial = 1; parcial < total; parcial++) {
                Thread.Sleep(30);
                OnAtualizouProgresso(parcial, total);
                if (Mestre.CancellationPending) {
                    return;
                }
            }

        }

    }
}
