using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Net;

namespace Miotec.Vert3d.Faturamento
{
    public class FaturamentoService {

        IFaturamentoAcessoLocal AcessoLocal {
            get {
                if (_acesso_local == null)
                    _acesso_local = new AcessoLocalSQLite();
                return _acesso_local;
            }
        }
        IFaturamentoAcessoLocal _acesso_local;

        IFaturamentoAcessoRemoto AcessoRemoto {
            get {
                if (_acesso_remoto == null)
                    _acesso_remoto = new AcessoRemotoDummy();
                return _acesso_remoto;
            }
        }
        IFaturamentoAcessoRemoto _acesso_remoto;



        public bool PodeFazerExame { get; private set; }

        TimeSpan TEMPO_LIMITE { get { return new TimeSpan(7, 0, 0, 0); } }


        // CONSTRUTOR
        public FaturamentoService() {
            
            Inicializar();

        }



        public void Inicializar() {
            if (ExistemPendencias()) {
                if (TentaSincronizarPendencias()) {
                    AtualizaDataUltimaConexao();
                    PodeFazerExame = true;
                } else {
                    if (DataUltimaConexaoAindaValida()) {
                        PodeFazerExame = true;
                    } else {                        
                        RequerAcaoUsuario();
                        PodeFazerExame = false;
                    }
                }
            }
        }


        private void RequerAcaoUsuario()
        {
            MessageBoxResult resposta = MessageBox.Show("Verifique sua conexão de internet e tente novamente.",
                                          "Problemas na conexão ao servidor",
                                          MessageBoxButton.OKCancel,
                                          MessageBoxImage.Exclamation);
            if (resposta == MessageBoxResult.OK) {
                //_repositorio_pacientes.Excluir(PacienteSelecionado);
                //ListaPacientes.Remove(PacienteSelecionado);
            }
        }

        private bool DataUltimaConexaoAindaValida()
        {
            return AcessoLocal.UltimaConexao < (DateTime.Now - TEMPO_LIMITE);
        }


        private void AtualizaDataUltimaConexao()
        {
            AcessoLocal.AtualizaDataUltimaConexao();
        }



        private bool ExistemPendencias()
        {
            return AcessoLocal.PossuiExamesPendentes();
        }
        
        private bool TentaSincronizarPendencias() {
            if (AcessoLocal.listaColetas.Count > 0)
            {
                if(VerificaConexaoUsuario())
                {
                    for(int i = 0; i < AcessoLocal.listaColetas.Count; i++)
                    {
                        AcessoRemoto.AdicionaNovaLinha(AcessoLocal.listaColetas[i].DataColeta, AcessoLocal.listaColetas[i].Nome, AcessoLocal.listaColetas[i].StatusExame);
                    }
                    return true;
                    }
            }
            return false;
        }

        public static bool VerificaConexaoUsuario()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
