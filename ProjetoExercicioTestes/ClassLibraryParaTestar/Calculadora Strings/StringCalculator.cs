using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryParaTestar
{
    public class StringCalculator
    {
        public int Add(String numbers)
        {

            if (numbers == null || numbers == "")
            {
                return 0;
            }

            int resultado = 0;


            String[] numeros = numbers.Split(',', '\n');

            foreach (String n in numeros)
            {
                int i = 0;
                try
                {
                    i = int.Parse(n);
                }
                catch
                {
                    throw new ArgumentoInvalidoException("Argumento inválido.");
                }
                resultado = resultado + i;
            }


            return resultado;
        }
    }
}
