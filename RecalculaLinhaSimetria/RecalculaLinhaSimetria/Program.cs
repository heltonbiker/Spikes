using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Miotec.Vert3d.DomainModel
using Miotec.Vert3d.Application.Model;

namespace RecalculaLinhaSimetria
{
    class Program
    {
        static void Main(string[] args)
        {

            string pasta = @"C:\Miotec\Vert3d\Exames";

            var listapastaexames = Directory.GetDirectories(pasta);

            foreach (string pastaexame in listapastaexames.OrderByDescending(x => x)) {
                
                //Exame exame;



                //var malha = exame.m

                //ModeloSimetriaClassico simetria = new ModeloSimetriaClassico(malha, @"C:\Miotec\Vert3d\temp");
                
            }

        }
    }
}
