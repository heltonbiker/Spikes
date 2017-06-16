using System;
using System.Windows.Media.Media3D;


namespace Miotec.Vert3d.DomainModel
{
    
    /// <summary>
    /// Contém os parâmetros de calibração da câmera.
    /// </summary>
    /// <remarks>
    /// <para>O sistema deve ler parâmetros intrínsecos da câmera diretamente do componente de acesso a hardware.
    /// A convenção é de que a câmera vai estar em formato retrato (maior dimensão na altura).</para>
    /// <para>Os parâmetros extrínsecos (posição da câmera em relação ao projetor) devem ser gerados em procedimento
    /// de calibração a ser implementado no código cliente (componente que utiliza esta classe)</para>
    /// </remarks>
    public class Calibracao {

        /// <summary>
        /// Largura (menor dimensão) do sensor da câmara, em pixels.
        /// </summary>
        public int AlturaPixelsCamera { get; private set; }

        /// <summary>
        /// Altura (maior dimensão) do sensor da câmara, em pixels.
        /// </summary>
        public int LarguraPixelsCamera { get; private set; }

        /// <summary>
        /// Distância entre o centro óptico da câmera e o centro do Plano de Projeção Virtual.
        /// </summary>
        /// <remarks>
        /// O sistema óptico das câmeras fotográficas pode ser completamente modelado determinando-se
        /// um Centro Óptico, a partir do qual irradia-se um feixe de raios (vetores) que passam por cada um dos pixels.
        /// Assim sendo, para facilitar a representação numérica, convenciona-se que a Distância Focal Virtual é a distância entre o Centro Óptico
        /// e um plano onde cada pixel tenha o tamanho aparente de um milímetro, devendo ser determinada experimentalmente
        /// conforme procedimento abaixo:
        /// 
        /// (inserir figura)
        /// </remarks>
        public double DistanciaFocalVirtual { get; private set; }

        /// <summary>
        /// Matriz gerada pelo procedimento de calibração,
        /// representando a orientação espacial da câmera em relação ao projetor.
        /// </summary>
        public Matrix3D MatrizRotacaoCamera { get; private set; }

        /// <summary>
        /// Vetor gerado pelo procedimento de calibração,
        /// representando a posição do centro óptico da câmera em relação ao projetor.
        /// </summary>
        public Vector3D VetorTranslacaoCamera { get; private set; }



        // CONSTRUTOR
        public Calibracao() {

            // MOCK: todo o construtor está com valores hard-coded
            AlturaPixelsCamera = 1024;
            LarguraPixelsCamera = 768;
            DistanciaFocalVirtual = 2300; // 2300 é um valor temporário, arbitrário

            var ang = -25 * (Math.PI/180.0);
            MatrizRotacaoCamera =  new Matrix3D ( 1,              0,              0, 0,
                                                          0,  Math.Cos(ang),  Math.Sin(ang), 0,
                                                          0, -Math.Sin(ang),  Math.Cos(ang), 0,
                                                          0,              0,              0, 1);

            VetorTranslacaoCamera = new Vector3D (0, 1200, -100);
        }



    }
}
