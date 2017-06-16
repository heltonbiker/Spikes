using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImagePlaceholderColorBar
{
    public abstract class MapaCorBase {


        protected abstract int[,] _array_cores { get; }
    
        /// <summary>
        /// mapeia valor entre 0 e 1 para valor entre 0 e 256
        /// </summary>
        protected int _tobyte(double posição) {
            int result = (int)(posição * 256);

            if (result > 255) result = 255;
            if (result < 0) result = 0;
            
            return result;
        }
        

        public Color obter_cor(double posição)
        {                                   
            int i = _tobyte(posição);
            return Color.FromArgb(255, _array_cores[i,0],
                                       _array_cores[i,1],
                                       _array_cores[i,2]);
        }

        public Color obter_gray(double posição) {
            int i = _tobyte(posição);
            return Color.FromArgb(255, i, i, i);
        }


        protected double sigmóide (double num) {
            // usada uma função sigmóide (atan) para que o
            // contraste (derivada) ao redor do zero seja 1, e
            // os valores muito altos ou baixos
            // convirjam assintoticamente para 1
            var ip = 1 / Math.PI;
            return 2*Math.Atan(2*num*ip)*ip;
        }

        protected double fator_contraste = 40;


    }
}
