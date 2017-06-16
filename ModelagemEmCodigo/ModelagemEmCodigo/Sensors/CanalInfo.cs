using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelagemEmCodigo
{
    public class CanalInfo
    {
        public readonly int TaxaDeAmostragem;

        public readonly int Resolução;

        public readonly UnidadeMedida Unidade;


        public int MaximoDigital { get; set; }
        public int MinimoDigital { get; set; }

        public double MaximoFisico { get; set; }
        public double MinimoFisico { get; set; }


        //public double ValorFísico(double valorDigital)
        //{
        //    double dFis = MaximoFisico - MinimoFisico;
        //    double dDigi = MaximoDigital - MinimoDigital;
        //    return MinimoFisico + dFis * (valorDigital - MinimoDigital) / dDigi;
        //}


        //public double ValorFísico(double valorDigital)
        //{
        //    return A * valorDigital + B;
        //}

        public double A
        {
            get
            {
                return (MaximoFisico - MinimoFisico)/(MaximoDigital - MinimoDigital);
            }
        }

        public double B
        {
            get
            {
                return MinimoFisico - A * MinimoDigital;
            }
        }
    }
}
