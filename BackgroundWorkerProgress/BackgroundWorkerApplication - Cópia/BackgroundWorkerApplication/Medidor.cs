using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackgroundWorkerApplication
{
    class Medidor : SubTarefa
    {

        static int divisor;
        public int Divisor { get { return divisor; } }

        internal static void Somador(int valor)
        {
            divisor += valor;
        }

        static public double DistribuidorTempo(double valor)
        {

            return valor / divisor; ;
        }

    }
}
