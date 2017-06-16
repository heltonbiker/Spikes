using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GerenciadorDeColeta
{
    public class PadraoDeProjecao : INotifyPropertyChanged {

        // entradas

        // ESTAS DEVEM SER INFERIDAS A PARTIR DO PROJETOR
        public int altura_projetor;
        public int largura_projetor;

        public int espessura_principal_pix;
        public int espessura_adicional_pix;
        public int intervalo_pix;

        // parâmetros
        public double deslocamento_inicial;
        public double deslocamento_adicional;
        
        public int num_franjas;
        
        public double posicao_principal_final;
        
        public Dictionary<string, double> posicoes_franjas = new Dictionary<string,double>();
        
        public double posicao_superior_norm = 1.0;
        public double posicao_inferior_norm = 0.0;



		public PadraoDeProjecao() {

            altura_projetor = 600;
            largura_projetor = 800;

            espessura_principal_pix = 6;
            espessura_adicional_pix = 3;
            intervalo_pix = 11;

		}


        public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String info)	{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}


    }
}
