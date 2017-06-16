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
            int numero_atividades = new Random().Next(100, 200);
            for (int i = 1; i < numero_atividades; i++) {
                Thread.Sleep(30);
                OnAtualizouProgresso(i, numero_atividades);
                if (Mestre.CancellationPending) {
                    return;
                }
            }

        }

    }
}
