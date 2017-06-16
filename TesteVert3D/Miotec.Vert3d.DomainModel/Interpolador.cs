using System.Windows.Media.Media3D;

namespace Miotec.Vert3d.DomainModel
{
    
    /// <summary>
    /// Representa um modelo de interpolação,
    /// gerado a partir de uma nuvem de pontos,
    /// capaz de gerar novos pontos em uma superfície.
    /// </summary>
    /// <remarks>
    /// Implementado com o método RBF ("Radial Basis Function")
    /// da biblioteca "alglib".
    /// </remarks>
    public class Interpolador {

        // Parâmetros livres
        const int CAMADAS_RBF = 6;
        const double RAIO_INICIAL_RBF = 100;
        const double RAIO_BUSCA_KDTREE = 20;
        const int MINIMO_VIZINHOS_KDTREE = 1;

        private Point3DCollection _nuvem;

        /// <summary>
        /// Projeção frontal (plano XY) da nuvem. Serve para a determinação das regiões
        /// que estão "dentro" e "fora" da malha.
        /// </summary>
        /// <remarks>
        /// Embora os interpoladores tipicamente modelem funções contínuas no plano XY,
        /// devem ser considerados apenas pontos que se situem sobre a superfície do dorso.
        /// </remarks>
        private alglib.kdtree _projecao_nuvem_pontos;

        /// <summary>
        /// Modelo matemático de interpolação.
        /// </summary>
        /// <remarks>
        /// Neste caso, é um modelo RBF (Radial Basis Function), mas há outros modelos possíveis
        /// (Kriging, splines, etc.).
        /// </remarks>
        private alglib.rbfmodel _modelo;



        // CONSTRUTOR
        public Interpolador (Point3DCollection nuvem) {
            this._nuvem = nuvem;
        }



        /// <summary>
        /// Inicializa o modelo de interpolação,
        /// a partir de certos parâmetros específicos de cada modelo.
        /// </summary>
        public void ConstruirModelo() { 



            // Transferindo os pontos da lista de pontos (que é uma Lista)
            // para a nuvem de pontos (que é um array)
            int numero_de_pontos = _nuvem.Count;
            var array_nuvem = new double[numero_de_pontos, 3];
            for (int i = 0; i < numero_de_pontos; i++) {
                var ponto = _nuvem[i];
                array_nuvem[i,0] = ponto.X;
                array_nuvem[i,1] = ponto.Y;
                array_nuvem[i,2] = ponto.Z;
            }


            // Construindo o modelo propriamente dito
            alglib.rbfcreate(2, 1, out _modelo);

            alglib.rbfsetpoints(_modelo, array_nuvem);

            alglib.rbfsetalgomultilayer(_modelo, RAIO_INICIAL_RBF, CAMADAS_RBF);

            alglib.rbfreport report;
            alglib.rbfbuildmodel(_modelo, out report);



            // Criando o array 2D que representa a projeção dos pontos da nuvem no plano XY
            double [,] projection = new double[array_nuvem.GetLength(0), 2];
            for (int i = 0; i < array_nuvem.GetLength(0); i++) {
                for (int j = 0; j < 2; j++) {
                    projection[i,j] = array_nuvem[i,j];
                }
            }

            alglib.kdtreebuild(projection, 2, 0, 2, out _projecao_nuvem_pontos);
                    
        }


        /// <summary>
        /// Solicita um novo ponto ao Interpolador.
        /// </summary>
        /// <param name="x">Coordenada X do ponto desejado.</param>
        /// <param name="y">Coordenada Y do ponto desejado.</param>
        /// <returns>Ponto com as coordenadas (X, Y, Z), sendo Z calculado a partir de X e Y.</returns>
        public double InterpolarCoordenadaZ(double x, double y) {
            
            double resultado = double.NaN;
            
            int vizinhos = alglib.kdtreequeryrnn(_projecao_nuvem_pontos, new double[] {x, y}, RAIO_BUSCA_KDTREE);                    
            if (vizinhos >= MINIMO_VIZINHOS_KDTREE)
                resultado = alglib.rbfcalc2(_modelo, x, y);

            return resultado;
        }

    }
}
