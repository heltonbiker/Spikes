using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miotec.Vert3d.Faturamento
{
    interface IFaturamentoAcessoRemoto {

        void AdicionaNovaLinha(int data, String nome, bool status);

    }
}