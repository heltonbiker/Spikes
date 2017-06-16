using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;



namespace Miotec.Vert3d.DomainModel
{
        
    /// <summary>
    /// Representação numérica,
    /// em forma de malha retangular estruturada,
    /// da superfície tridimensional do dorso
    /// (incluindo marcadores de estereometria)
    /// </summary>
    public class Malha {

        private const double ESPACAMENTO = 5;
        
        private readonly Point3DCollection _nuvem;
        private readonly Marcadores _marcadores;

        private Interpolador _interpolador;

        private Double [,] _matriz_relevo;
        private Point _origem;

     
        //public Marcadores Marcadores { get { return _marcadores; } }

        /// <summary>
        /// Matriz representando uma malha estruturada, regular, tridimensional.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Esta matriz (array numérico) contém explicitamente apenas o valor das coordenadas Z.
        /// O valor das coordenadas X e Y deve ser calculado a partir dos índices [I,J] de cada valor
        /// da matriz, dos valores OrigemX e OrigemY, e do valor de Espacamento.
        /// </para><para>
        /// A origem das linhas e colunas de matrizes, [I,J], é o canto superior esquerdo,
        /// mas a origem do sistema de coordenadas [X,Y] é o canto INFERIOR esquerdo.
        /// Assim sendo, as coordenadas Y e o índice I crescem em sentidos opostos.
        /// </para>
        /// </remarks>
        public Double [,] MatrizRelevo { get { return _matriz_relevo; } }      
        
        /// <summary>
        /// Quantidade de linhas ao longo da altura da MatrizRelevo.
        /// </summary>
        public int Linhas { get { return _matriz_relevo.GetLength(0); } }

        /// <summary>
        /// Quantidade de colunas ao longo da largura da MatrizRelevo.
        /// </summary>
        public int Colunas { get { return _matriz_relevo.GetLength(1); } }

        /// <summary>
        /// Espaçamento entre as colunas e linhas da matriz, em milímetros.
        /// </summary>
        public Double Espacamento { get { return ESPACAMENTO; } }

        /// <summary>
        /// Coordenada X dos pontos da primeira coluna da malha (coluna mais à esquerda).
        /// </summary>
        public Double OrigemX { get { return _origem.X; } }

        /// <summary>
        /// Coordenada Y dos pontos da primeira linha (coluna mais abaixo).
        /// </summary>
        public Double OrigemY { get { return _origem.Y; } }


        /// <summary>
        /// Converte o objeto malha em um array de pontos 3D.
        /// </summary>
        /// <returns>Um array de arrays onde
        /// linhas e colunas são preenchidas com Point3D.
        /// </returns>
        /// <remarks>
        /// Usado para serialização e exibição, evitando a necessidade
        /// de usar um objeto de domínio Malha ao longo de toda a aplicação.
        /// Não foi usado um array bidimensional ([,]) porque não há suporte
        /// para serialização desse tipo de array no .NET.
        /// </remarks>
        public Point3D[][] getArraysRelevo() {
            var resultado = new Point3D[Linhas][];
            for (int i = 0; i < Linhas; i++) {
                var arraylinha = new Point3D[Colunas];
                for (int j = 0; j < Colunas; j++) {
                    double x = OrigemX +       Linhas  * j;
                    double y = OrigemY + (Colunas - i) * ESPACAMENTO;
                    double z = _matriz_relevo[i,j];
                    arraylinha [j] = new Point3D(x,y,z);
                }
                resultado[i] = arraylinha;
            }
            return resultado;
        }

        /// <summary>
        /// Retorna um array de arrays ("jagged array", [][])
        /// a partir do mapa de curvatura no formato matriz ([,])
        /// </summary>
        /// <remarks>
        /// Usado especificamente para serialização em XML,
        /// já que não há suporte para serialização de array bidimensional.
        /// </remarks>
        public double[][] getArraysCurvatura() {
            var resultado = new double[Linhas][];
            for (int i = 0; i < Linhas; i++) {
                var arraylinha = new double[Colunas];
                for (int j = 0; j < Colunas; j++) {
                    arraylinha [j] = _matriz_relevo[i,j];
                }
                resultado[i] = arraylinha;
            }
            return resultado;
        }



        // CONSTRUTOR
        public Malha(Point3DCollection nuvem, Marcadores marcadores) {
            this._nuvem = nuvem;
            this._marcadores = marcadores;
        }




        /// <summary>
        /// Faz a interpolação da malha de relevo, utilizando um <see cref="Interpolador"/>.
        /// </summary>
        public void Construir() {

            const int A_CADA_TANTOS_PONTOS = 5;

            double compensaZ = _nuvem.Select(p => p.Z).Min();

            Point3DCollection nuvem_reduzida = new Point3DCollection( _nuvem
                                                                      .Where((v, i) => i % A_CADA_TANTOS_PONTOS == 0)
                                                                      //.Select(p => new Point3D(p.X, p.Y, p.Z-compensaZ))
                                                                      .ToList<Point3D>() );

            _interpolador = new Interpolador(nuvem_reduzida);
            _interpolador.ConstruirModelo();

            int número_de_pontos = nuvem_reduzida.Count();
            var rangepontos = Enumerable.Range(0, número_de_pontos);

            var valoresX = rangepontos.Select(i => nuvem_reduzida[i].X);
            double xmin = valoresX.Min();
            double xmax = valoresX.Max();

            var valoresY = rangepontos.Select(i => nuvem_reduzida[i].Y);
            double ymin = valoresY.Min();
            double ymax = valoresY.Max();

            _origem = new Point(xmin, ymin);
            
            int colunas = (int)Math.Ceiling((xmax-xmin)/ESPACAMENTO);
            int linhas =  (int)Math.Ceiling((ymax-ymin)/ESPACAMENTO);
          
            var coordenadas_x = new double[colunas];
            for (int xi = 0; xi < colunas; xi++) coordenadas_x[xi] = xmin + xi*ESPACAMENTO;

            var coordenadas_y = new double[linhas];
            for (int yi = 0; yi <  linhas; yi++) coordenadas_y[yi] = ymin + yi*ESPACAMENTO;
            

            _matriz_relevo = new double[linhas, colunas];

            for (int linha = 0; linha < linhas; linha++) {
                for (int coluna = 0; coluna < colunas; coluna++) {
                    _matriz_relevo[linha, coluna] =
                        _interpolador.InterpolarCoordenadaZ(coordenadas_x[coluna],
                                                      coordenadas_y[linha]);
                }
            }

        }




        /// <summary>
        /// Faz a interpolação de um ponto tridimensional
        /// a partir das coordenadas de um ponto bidimensional válido (situado
        /// "dentro" da projeção XY da malha)
        /// </summary>
        public Point3D getPonto3D(Point ponto2D)
        {
            double x = ponto2D.X;
            double y = ponto2D.Y;
            double z = _interpolador.InterpolarCoordenadaZ(x, y);
            return new Point3D(x, y, z);
        }

        /// <summary>
        /// Calcula a curvatura de superfície de um array representando o relevo do dorso.
        /// </summary>
        /// <returns>Um array com os valores de curvatura de superfície, com as mesmas dimensões do array original</returns>
        public double[,] getMatrizCurvatura() {
            double[,] Zi  = gradiente(_matriz_relevo, 0);
            double[,] Zj  = gradiente(_matriz_relevo, 1);
            double[,] Zii = gradiente(Zi, 0);
            double[,] Zjj = gradiente(Zj, 1);
            double[,] Zij = gradiente(Zi, 1);
            
            double[,] H = new double[_matriz_relevo.GetLength(0),
                                     _matriz_relevo.GetLength(1)];

            for (int i = 0; i < _matriz_relevo.GetLength(0); i++) {
                for (int j = 0; j < _matriz_relevo.GetLength(1); j++) {
                    double Zi2 = Zi[i,j]*Zi[i,j];
                    double Zj2 = Zj[i,j]*Zj[i,j];
                    double N = (Zj2 + 1)*Zii[i,j] - 2*Zi[i,j]*Zj[i,j]*Zij[i,j] + (Zi2 + 1)*Zjj[i,j];
                    double D = 2 * Math.Pow((Zj2 + Zi2 + 1), 1.5);
                    H[i,j] = -N/D;
                }
            }

            return H;
        }


        /// <summary>
        /// Calcula o gradiente de uma matriz bidimensional, ao longo de um eixo determinado.
        /// </summary>
        /// <param name="array">Matriz de entrada.</param>
        /// <param name="dimension">Dimensão ao longo da qual o gradiente deve ser calculado</param>
        /// <returns>Matriz de saída.</returns>
        private double[,] gradiente(double[,] array, int dimension) {
            var altura = array.GetLength(0);
            var largura = array.GetLength(1);

            var result = new double[altura, largura];

            var otherDimension = 1 - dimension;
            var otherDimensionLength = array.GetLength(otherDimension);
            var dimensionLength = array.GetLength(dimension);
            for (int i = 0; i < dimensionLength; i++) {
                for (int j = 0; j < otherDimensionLength; j++) {

                    // O segredo é são as funções GetValue e SetValue
                    // que tratam arrays de inteiros como se fossem índices
                    var setIndexes = new int[2] { j, j };
                    setIndexes[dimension] = i;

                    var beforeIndexes = new int[2] { j, j };
                    beforeIndexes[dimension] = Math.Max(i - 1, 0);

                    var afterIndexes = new int[2] { j, j };
                    afterIndexes[dimension] = Math.Min(i + 1, dimensionLength - 1);

                    var beforeValue = (double)array.GetValue(beforeIndexes);
                    var afterValue = (double)array.GetValue(afterIndexes);

                    result.SetValue( ((afterValue - beforeValue) * 0.5)/ESPACAMENTO, setIndexes);
                }
            }

            return result;
        }

    }
}
