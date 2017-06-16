using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miotec.Vert3d.Faturamento;

namespace TestandoVertFaturamento
{
    class Program
    {
        static void Main(string[] args)
        {
            var faturamento = new FaturamentoService();

            Console.WriteLine(faturamento.PodeFazerExame);
            
        }
    }
}
