
using System;
using System.Collections.Generic;
namespace Miotec.Vert3d.Faturamento
{
    internal interface IFaturamentoAcessoLocal {

        DateTime UltimaConexao { get; }

        List<ColetasFeitas> listaColetas {get; set;}

        bool PossuiExamesPendentes();

        void AtualizaDataUltimaConexao();

        void AdicionaPacientesSQLite();



    }
}
