using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Miotec.Vert3d.DomainModel
{
    
    /// <summary>
    /// Contém métodos e armazena dados relativos à transformação de dados bidimensionais (foto franjas)
    /// em dados tridimensionais (nuvem de pontos) através da técnica de Projeção de Luz Estruturada com franjas.
    /// </summary>
    internal class EstruturaFranjas : List<EstruturaFranjas.Franja> {

        
        private readonly System.Drawing.Bitmap _foto_franjas;
        private readonly List<Double> _angulos_projecao;
        private readonly int _indice_principal_projecao;
        private readonly Marcadores _marcadores;        
        private readonly Calibracao _calibracao;

        private int _indice_principal_detectado;
        private int _angulo_compensacao_rotacao;

        /// <summary>
        /// Ângulo que representa a rotação incial global da nuvem de pontos,
        /// conforme detectado e corrigido por <see cref="CompensarRotacao"/>
        /// </summary>
        public int AnguloCompensacaoRotacao { get { return _angulo_compensacao_rotacao; } }


        // CONSTRUTOR
        public EstruturaFranjas (System.Drawing.Bitmap foto_franjas,
                                 List<Double> angulos_projecao,
                                 int indice_principal_projecao,
                                 Calibracao calibracao,
                                 Marcadores marcadores) : base() {

            this._foto_franjas = foto_franjas;
            this._angulos_projecao = angulos_projecao;
            this._indice_principal_projecao = indice_principal_projecao;
            this._calibracao = calibracao;
            this._marcadores = marcadores;
        }




        /// <summary>
        /// Coordena a execução, em determinada ordem, das etapas de processamento de franjas
        /// </summary>
        /// <remarks>
        /// O processamento envolve as seguintes etapas sequenciais não-interativas:
        /// <list type="number">
        /// <item><description>
        /// Detecção da linha média das franjas a partir da imagem binarizada;
        /// </description></item>
        /// <item><description>
        /// Mapeamento de um ângulo de projeção (determinado pelo Padrão de Projeção) para cada franja;
        /// </description></item>
        /// <item><description>
        /// Extração dos pontos 3D propriamente ditos
        /// </description></item>
        /// </list>
        /// </remarks>
        public void Executar() {

            Logger.Log(LoggingLevel.Debug, "Iniciando DetectarFranjas");
            this.DetectarFranjas();
            Logger.Log(LoggingLevel.Debug, "Finalizando DetectarFranjas");

            Logger.Log(LoggingLevel.Debug, "Iniciando AtribuirAngulos");            
            this.AtribuirAngulos();
            Logger.Log(LoggingLevel.Debug, "Finalizando AtribuirAngulos");

            Logger.Log(LoggingLevel.Debug, "Iniciando ExtrairPontos3d");            
            this.ExtrairPontos3d();
            Logger.Log(LoggingLevel.Debug, "Finalizando ExtrairPontos3d");

            Logger.Log(LoggingLevel.Debug, "Iniciando FExtrairMarcadores3d");            
            this.ExtrairMarcadores3d();
            Logger.Log(LoggingLevel.Debug, "Finalizando ExtrairMarcadores3d");


            //this.CompensarRotacao();

        }




        /// <summary>
        /// Utiliza um <see cref="DetectorFranjas"/> para
        /// obter as linhas principais a partir das imagens de entrada
        /// </summary>
        private void DetectarFranjas() {
        
            // MOCK: a largura inicial deve ser correta (marcadores.VP)
            var _detector = new DetectorFranjas(_foto_franjas, (_foto_franjas.Width/2));

            _detector.Executar();
            
            var franjas_detectadas = _detector.ListaFranjas;

            _indice_principal_detectado = _detector.IndiceFranjaPrincipal;

            foreach (List<Point> fr in franjas_detectadas) {
                var newfranja = new Franja();
                foreach (Point pt in fr) {
                    newfranja.Add(new PontoEstereometria(pt.Y, pt.X));
                }
                this.Add(newfranja);
            }
            
        }


        /// <summary>
        /// Utiliza os dados de <see cref="Projecao"/> para
        /// determinar o ângulo de projeção de cada franja
        /// </summary>
        private void AtribuirAngulos() {        

            for (int f = 0; f < this.Count; f++) {
                int indiceNaProjecao = _indice_principal_projecao + _indice_principal_detectado - f;
                this[f].Angulo = _angulos_projecao[indiceNaProjecao];
            }        
        }


        /// <summary>
        /// Extrai a posição 3D de cada <see cref="PontoFranja"/>
        /// </summary>
        /// <remarks>
        /// A técnica de estereometria por luz estruturada
        /// consiste nos seguintes passos:
        /// <list type="number">
        /// <item>
        /// <description>
        /// Modelar cada listra projetada pelo projetor como sendo um plano geométrico
        /// definido pelo Centro Óptico do projetor (origem do sistema de coordenadas)
        /// e pela direção do vetor normal a esse plano, resultado da rotação de um vetor
        /// unitário vertical ao redor da origem, por um ângulo equivalente ao ângulo
        /// de projeção da respectiva listra.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Modelar cada pixel da imagem capturada como um vetor entre
        /// o Centro Óptico da câmera e a posição desse pixel no
        /// Plano de Projeção Virtual da câmera.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Realizar a operação de <see cref="IntersecaoRetaPlano"/>
        /// entre esse plano e esse vetor.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        private void ExtrairPontos3d() {
            
            foreach (var franja in this) {
                foreach (var ponto in franja) {

                    Vector3D raio_ccd_primitivo = new Vector3D(ponto.J - _calibracao.LarguraPixelsCamera*0.5,
                                                               _calibracao.AlturaPixelsCamera*0.5 - ponto.I,
                                                              -_calibracao.DistanciaFocalVirtual);
                    
                    Vector3D raio_ccd = Vector3D.Multiply(raio_ccd_primitivo, _calibracao.MatrizRotacaoCamera);
                    
                    Vector3D vetor_normal = vetor_normal_plano_franja(franja.Angulo);

                    Point3D interseção = IntersecaoRaioFranja(vetor_normal, _calibracao.VetorTranslacaoCamera, raio_ccd);
                
                    ponto.X = interseção.X;
                    ponto.Y = interseção.Y;
                    ponto.Z = interseção.Z;
                }
            }        
        }


        /// <summary>
        /// Calcula as coordenadas X,Y,Z de cada marcador,
        /// utilizando a função <see cref="extrairMarcador"/>.
        /// </summary>
        private void ExtrairMarcadores3d() {
        
            extrairMarcador(_marcadores.VP);
            extrairMarcador(_marcadores.DL);
            extrairMarcador(_marcadores.DR);
            extrairMarcador(_marcadores.SP);
        
        }




        /// <summary>
        /// Avalia e compensa a rotação principal da nuvem de pontos ao redor de
        /// um vetor unitário alinhado com o eixo Y, com origem no Baricentro da malha.
        /// </summary>
        /// <remarks>
        /// Funciona achando, para cada franja, a dupla-tangente da franja, se houver.
        /// A média dos ângulos das duplas-tangentes é usado para
        /// compensar a rotação Y da malha ao redor de seu baricentro.
        /// </remarks>
        public void CompensarRotacao() {

            throw new NotImplementedException();

            foreach (Franja fr in this) {

                List<int> linha = upperHull(fr);

                int indiceEsq = 0;
                int indiceDir = fr.Count-1;

                double maxdist = 0;
                for (int i = 1; i < fr.Count-1; i++) {
                    var p1 = fr[i-1].to3d;
                    var p2 = fr[i].to3d;
                    double dist = (p2 - p1).Length;
                    if (dist > maxdist) {
                        maxdist = dist;
                        indiceEsq = i-1;
                        indiceDir = i;
                    }
                }

                ////// ESTA FUNÇÃO DEVERIA SER NA VERDADE O "DETECÇÃO DE DUPLAS TANGENTES",
                ////// QUE DEPOIS SERIA USADO PARA ACHAR O ÂNGULO DE COMPENSAÇÃO.

                //double angulo = Math.Atan2(
            }

            

            // atribuir o resultado à propriedade AnguloCompensacaoRotacao

        }


        /// <summary>
        /// Nuvem de pontos a ser passada para outros objetos, como saída final do processamento de franjas.
        /// </summary>
        /// <returns>Nuvem de pontos não-estruturada.</returns>
        public Point3DCollection getNuvem() {
            var resultado = new Point3DCollection();
            foreach (Franja f in this) {
                foreach (PontoEstereometria pf in f) {
                    resultado.Add(pf.to3d);
                }
            }
            return  resultado;     
        }




        /// <summary>
        /// Detecta a interseção geométrica entre o plano da listra projetada
        /// e o vetor que passa por um determinado pixel da imagem capturada
        /// </summary>
        /// <param name="origemPlano">Centro óptico do projetor,
        ///     que por definição é a origem do sistema de coordenadas.</param>
        /// <param name="normalPlano">Vetor normal ao plano de projeção,
        ///     com origem no centro óptico do projetor, apontando para cima (Y positivo).</param>
        /// <param name="origemRaio">Centro Óptico da câmera,
        ///     que por definição é a posição definida na propriedade VetorTranslacaoCamera de <see cref="Calibracao"/>.</param>
        /// <param name="raio">Vetor com origem no Centro Óptico da câmera,
        ///     e extremidade na posição do pixel sobre o Plano de Projeção Virtual da câmera.</param>
        /// <returns></returns>
        private Point3D IntersecaoRaioFranja (Vector3D normalPlano, Vector3D origemRaio, Vector3D raio) {
            
            double D =  Vector3D.DotProduct(normalPlano, raio);
            double N = -Vector3D.DotProduct(normalPlano, origemRaio);
            Point3D intersecao = (Point3D)origemRaio + (N/D)*raio;
            
            return intersecao;
        }


        /// <summary>
        /// Extrai as coordenadas X,Y,Z de um marcador a partir das coordenadas I e J.
        /// </summary>
        /// <param name="marc"></param>
        private void extrairMarcador(PontoEstereometria marc) {
            
            int indice = Convert.ToInt32(marc.J) - 1; // Necessário pois o valor das colunas começa em "1"

            // vamos ter que achar a coluna de PontosEstereometria que têm o mesmo X.
            // pela própria forma como a EstruturaFranjas é criada, os pontos já vão
            // vir em ordem decrescente de Y.
            var coluna = new List<PontoEstereometria>();
            foreach (var franja in this) {
                foreach (var ponto in franja) {
                    if (Convert.ToInt32(ponto.J) == Convert.ToInt32(marc.J))
                        coluna.Add(ponto);
                }
            }

            // agora tem que fazer uma interpolação (linear) entre os valores vizinhos de X e Y
            // 1) achar os dois valores vizinhos;
            // 2) achar o delta I entre eles e entre o marcador.I e o superior (ou inferior, mas vamos fazer com o superior);
            // 3) fazer regra de três para achar os valores de X, Y e Z (o valor de Z pode ser reinterpolado mais tarde, na malha)
            var acima = new PontoEstereometria(0,0);
            var abaixo = new PontoEstereometria(0,0);
            for (int n = 0; n < coluna.Count; n++) {
                if (coluna[n].I < marc.I && coluna[n+1].I > marc.I) {
                    acima = coluna[n];
                    abaixo = coluna[n+1];
                    break;
                }
            }

            double ratio = (marc.I - acima.I) / (abaixo.I - acima.I);

            marc.X = acima.X.Value + ratio * (abaixo.X.Value - acima.X.Value);
            marc.Y = acima.Y.Value + ratio * (abaixo.Y.Value - acima.Y.Value);
            marc.Z = acima.Z.Value + ratio * (abaixo.Z.Value - acima.Z.Value);

        }


        /// <summary>
        /// Calcula a posição do centro geométrico da nuvem de pontos.
        /// </summary>
        private Point3D getCentroBoundingBox() {
            double xmax, xmin, ymax, ymin, zmax, zmin;
            xmin = ymin = zmin = double.MaxValue;
            xmax = ymax = zmax = double.MinValue;
            foreach (Franja fr in this) {
                foreach (PontoEstereometria pt in fr) {
                    if (pt.X.Value < xmin) xmin = pt.X.Value;
                    if (pt.X.Value < xmax) xmax = pt.X.Value;
                    if (pt.Y.Value < ymin) ymin = pt.Y.Value;
                    if (pt.Y.Value < ymax) ymax = pt.Y.Value;
                    if (pt.Z.Value < zmin) zmin = pt.Z.Value;
                    if (pt.Z.Value < zmax) zmax = pt.Z.Value;
                }
            }

            return new Point3D ((xmin+xmax)*0.5,
                                (ymin+ymax)*0.5,
                                (zmin+zmax)*0.5);
        }
        


        private List<int> upperHull (Franja fr) {
            var upper = new List<int> {0};
            for (int p = 0; p < fr.Count; p++) {
                while (upper.Count >=2 && crossProduct(fr[upper[upper.Count-2]],
                                                       fr[upper[upper.Count-1]],
                                                       fr[p]) <= 0)
                    upper.RemoveAt(upper.Count-1);
                upper.Add(p);
            }
            return upper;
        }

        private double crossProduct(PontoEstereometria p0, PontoEstereometria p1, PontoEstereometria p2) {
            return   (p1.X.Value - p0.X.Value) * (p2.Y.Value - p0.Y.Value)
                   - (p1.Y.Value - p0.Y.Value) * (p2.X.Value - p0.X.Value);
        }

        private Vector3D vetor_normal_plano_franja (double ângulo) {
            Matrix3D rotação = new Matrix3D ( 1,                 0,                 0, 0,
                                              0,  Math.Cos(ângulo),  Math.Sin(ângulo), 0,
                                              0, -Math.Sin(ângulo),  Math.Cos(ângulo), 0,
                                              0,                 0,                 0, 1);

            Vector3D ponto_vetor_normal = Vector3D.Multiply(new Vector3D(0,1,0), rotação);
            return ponto_vetor_normal;
        }




      
        /// <summary>
        /// Estrutura de dados representando uma franja de estereometria.
        /// </summary>
        internal class Franja : List<PontoEstereometria> {

            /// <summary>
            /// Ângulo de projeção da listra que deu origem a esta franja./>
            /// </summary>
            public double Angulo { get; set; }

        }




    }

}
