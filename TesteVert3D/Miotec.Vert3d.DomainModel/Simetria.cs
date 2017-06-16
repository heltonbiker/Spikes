using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using System.IO;

namespace Miotec.Vert3d.DomainModel
{
    
    /// <summary>
    /// Responsável por analisar e armazenar as informações de simetria
    /// de um objeto <see cref="Malha"/>.
    /// </summary>
    public class ModeloSimetria
    {        
        // Parâmetros livres
        const int AVANCO = 5;
        const int RAIO = 15;
        const int ZOOM = 2;
        const double DISTANCIA = 5;
        const int NIVEIS = 40;

        Malha _malha;

        List<ParSimetria> _pares;
        
        /// <summary>
        /// Linha tridimensional, pertencente à superfície do dorso,
        /// composta pela sequência longitudinal de pontos de simetria do dorso.
        /// </summary>
        /// <remarks>
        /// O princípio que fundamenta a existência de uma "linha de simetria"
        /// é de que o dorso humano é composto de um lado direito e um lado esquerdo.
        /// Assim sendo, e considerando que as alterações de postura se manifestam
        /// na forma de um relevo assimétrico, supõe-se que a linha de simetria contenha
        /// propriedades geométricas relacionadas às alterações da topografia do dorso.
        /// </remarks>
        public Point3DCollection LinhaSimetria { get; private set; }


        // CONSTRUTOR
        public ModeloSimetria(Malha malha) {
            this._malha = malha;
        }


        /// <summary>
        /// Realiza a análise de simetria da malha.
        /// </summary>
        public void Executar() {

            DetectarPares();

            ExtrairLinhaSimetria();

        }


        /// <summary>
        /// Associa pontos de um lado do dorso aos pontos de melhor correlação do lado oposto.
        /// </summary>
        /// <remarks>
        /// Funciona criando uma retícula quadrada de pontos candidatos, realizando a correlação
        /// cruzada entre a vizinhança de cada um desses pontos e a vizinhança do lado simétrico,
        /// atribuindo um valor para essa correlação.
        /// </remarks>
        private void DetectarPares() {

            _pares = new List<ParSimetria>();

            int velha_largura = _malha.Colunas;
            int velha_altura = _malha.Linhas;
            
            double [,] malha_curvatura = _malha.getMatrizCurvatura();


            // substituição dos NaNs por Zeros
            for (int i = 0; i < velha_altura; i++) {
                for (int j = 0; j < velha_largura; j++) {
                    if (double.IsNaN(malha_curvatura[i,j]))
                        malha_curvatura[i,j] = 0;
                }
            }

            // resampleando para uma maior resolução
            int nova_altura = velha_altura * ZOOM;
            int nova_largura = velha_largura * ZOOM;
            double[,] novamalha;
            alglib.spline2dresamplebicubic(malha_curvatura, velha_altura, velha_largura,
                                           out novamalha, nova_altura, nova_largura);


            // faz a varredura correlacionando um lado com o outro:
            // "para cada ponto de uma grade quadrada com lado = "avanço",
            //  posicionar uma janela fixa quadrada centrada nesse ponto"
            for (int centro_janela_fixa_j = 0; centro_janela_fixa_j < nova_largura; centro_janela_fixa_j += AVANCO) {
            for (int centro_janela_fixa_i = 0; centro_janela_fixa_i < nova_altura; centro_janela_fixa_i += AVANCO) {                    

                int inicio_largura_janela_fixa = Math.Max (0,       centro_janela_fixa_j-RAIO);
                int final_largura_janela_fixa  = Math.Min (nova_largura, centro_janela_fixa_j+RAIO);
                int inicio_altura_janela_fixa  = Math.Max (0,       centro_janela_fixa_i-RAIO);
                int final_altura_janela_fixa   = Math.Min (nova_altura,  centro_janela_fixa_i+RAIO);

                double pico_de_correlacao = double.MinValue;
                int i_pico = -1;
                int j_pico = -1;

                // correlação cruzada entre "janelinhas" do lado esquerdo e direito da malha:
                // "para cada pixel da janelinha fixa, centrar uma janelinha móvel que na verdade
                //  é a janela fixa refletida, e fazer a correlação da janela móvel com a imagem original"
                for (int centro_janela_movel_j = inicio_largura_janela_fixa; centro_janela_movel_j < final_largura_janela_fixa; centro_janela_movel_j++) {
                for (int centro_janela_movel_i = inicio_altura_janela_fixa; centro_janela_movel_i < final_altura_janela_fixa; centro_janela_movel_i++) {

                    int inicio_largura_janela_sobreposta = Math.Max(0,       centro_janela_movel_j-RAIO);
                    int final_largura_janela_sobreposta  = Math.Min(nova_largura, centro_janela_movel_j+RAIO);
                    int inicio_altura_janela_sobreposta  = Math.Max(0,       centro_janela_movel_i-RAIO);
                    int final_altura_janela_sobreposta   = Math.Min(nova_altura,  centro_janela_movel_i+RAIO);

                    double valor_correlacao = 0;

                    // nos laços "for" abaixo, efetua a correlação de fato
                    // (para uma dada posição da janela móvel sobre a janela fixa)
                    for (int pixel_j = inicio_largura_janela_sobreposta; pixel_j < final_largura_janela_sobreposta; pixel_j++) {
                    for (int pixel_i = inicio_altura_janela_sobreposta; pixel_i < final_altura_janela_sobreposta; pixel_i++) {

                        int pixel_refletido_i = pixel_i + (centro_janela_fixa_i - centro_janela_movel_i);
                        int pixel_refletido_j = (nova_largura - 1 - centro_janela_fixa_j) + centro_janela_movel_j - pixel_j;

                        if ( (pixel_refletido_i < 0) ||
                             (pixel_refletido_i > nova_altura - 1) ||
                             (pixel_refletido_j < 0) ||
                             (pixel_refletido_j > nova_largura - 1) )
                             continue;
                                    
                        double valor_original = novamalha[pixel_i, pixel_j];
                        double valor_refletido = novamalha[pixel_refletido_i, pixel_refletido_j];

                        valor_correlacao += valor_original * valor_refletido;

                    }
                    }

                    if (valor_correlacao > pico_de_correlacao) {
                        pico_de_correlacao = valor_correlacao;
                        i_pico = centro_janela_fixa_i - (centro_janela_movel_i - centro_janela_fixa_i);
                        j_pico = (nova_largura - centro_janela_fixa_j - 1) - (centro_janela_fixa_j - centro_janela_movel_j);
                    }
                }
                }

                double ymin = _malha.OrigemY;
                double xmin = _malha.OrigemX;
                double espacamento = _malha.Espacamento;

                var esq = new Point((centro_janela_fixa_j/ZOOM) * espacamento + xmin,
                                    (centro_janela_fixa_i/ZOOM) * espacamento + ymin);
                var dir = new Point((j_pico/ZOOM) * espacamento + xmin,
                                    (i_pico/ZOOM) * espacamento + ymin);
                
                _pares.Add(new ParSimetria(esq, dir, pico_de_correlacao));
            }
			}
        
        }

        /// <summary>
        /// Cria uma lista contendo um ponto, considerado o "ponto de simetria",
        /// para cada nível horizontal analisado.
        /// </summary>
        /// <remarks>
        /// Para cada nível horizontal, é analisado estatisticamente
        /// o conjunto dos pontos de simetria da vizinhança desse nível.
        /// </remarks>
        private void ExtrairLinhaSimetria() {

            LinhaSimetria = new Point3DCollection();
            
            double superior = _pares.Select(p => p.Medio.Y).Max();
            double inferior = _pares.Select(p => p.Medio.Y).Min();
            for (double Y = inferior; Y < superior; Y += (superior-inferior)/NIVEIS) {
                Point p = ExtrairSimetriaNivelHorizontal(Y);
                LinhaSimetria.Add(_malha.getPonto3D(p));
            }
        }

        
        /// <summary>
        /// Realiza a análise estatística de um determinado nível horizontal
        /// do conjunto de pontos de simetria.
        /// </summary>
        /// <remarks>
        /// Diversos métodos podem ser usados, aqui está sendo usad
        /// o "centro de massa" (média aritmética ponderada tridimensional).
        /// </remarks>
        private Point ExtrairSimetriaNivelHorizontal(double y) {
            
            double somax = 0;
            double somay = 0;
            double somac = 0;

            foreach (var ponto in _pares) {
                if (Math.Abs(ponto.Medio.Y - y) < DISTANCIA) {
                    somax += ponto.Medio.X * ponto.ValorCorrelacao;
                    somay += ponto.Medio.Y * ponto.ValorCorrelacao;
                    somac += ponto.ValorCorrelacao;
                }
            }

            double xmedio = somax/somac;
            double ymedio = somay/somac;

            return new Point(xmedio, ymedio);
        }



        /// <summary>
        /// Estrutura que representa a relação de simetria
        /// entre pontos correspondentes de cada lado do dorso.
        /// </summary>
        public class ParSimetria {

            private Point _esquerdo;
            private Point _direito;
            private Double _valor_correlacao;

            /// <summary>
            /// Posição do ponto esquerdo no plano XY.
            /// </summary>
            public Point Esquerdo { get { return _esquerdo; } }

            /// <summary>
            /// Posição do ponto direito no plano XY.
            /// </summary>
            public Point Direito { get { return _direito; } }

            /// <summary>
            /// Posição do ponto médio no plano XY.
            /// </summary>
            public Point Medio {
                get {
                    return new Point((_direito.X + _esquerdo.X)/2,
                                     (_direito.Y + _esquerdo.Y)/2);
                }
            }

            /// <summary>
            /// Valor adimensional representando a intensidade
            /// da correlação de simetria entre os pontos Esquerdo e Direito.
            /// </summary>
            public Double ValorCorrelacao { get { return _valor_correlacao; } }


            // CONSTRUTOR
            public ParSimetria(Point esq, Point dir, Double corr) {
                _esquerdo = esq;
                _direito = dir;
                _valor_correlacao = corr;
            }
        }

    }
}
