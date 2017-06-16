using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Miotec.Vert3d.DomainModel
{
    /// <summary>
    /// Estrutura de dados representando um ponto de estereometria.
    /// </summary>
    public class PontoEstereometria {

        /// <summary>
        /// Coordenada do pixel correspondente, na imagem capturada,
        /// ao longo do eixo vertical, de cima para baixo.
        /// </summary>
        public readonly double I;

        /// <summary>
        /// Coordenada do pixel correspondente, na imagem capturada,
        /// ao longo do eixo horizontal, da esquerda para a direita.
        /// </summary>
        public readonly double J;

        /// <summary>
        /// Coordenada X do ponto 3D calculado, com eixo X originando-se
        /// no centro óptico do projetor (origem do sistema) e orientado para a direita.
        /// </summary>
        public double? X = null;

        /// <summary>
        /// Coordenada Y do ponto 3D calculado, com eixo Y originando-se
        /// no centro óptico do projetor (origem do sistema) e orientado para cima.
        /// </summary>
        public double? Y = null;

        /// <summary>
        /// Coordenada Z do ponto 3D calculado, com eixo Z originando-se
        /// no centro óptico do projetor (origem do sistema, e orientado para TRÁS
        /// (do paciente em direção ao projetor).
        /// As coordenadas Z, portanto, sempre serão valores originalmente negativos.
        /// </summary>
        public double? Z = null;




        // CONSTRUTORES
        public PontoEstereometria (double i, double j) {
            this.I = i;
            this.J = j;
        }




        /// <summary>
        /// Ponto 3D que descreve a posição tridimensional calculada para o PontoFranja.
        /// </summary>
        public Point3D to3d {
            get {
                return new Point3D(X.Value, Y.Value, Z.Value);
            }
        }

    }
}
