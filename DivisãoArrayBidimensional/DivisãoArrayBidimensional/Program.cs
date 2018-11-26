using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisãoArrayBidimensional
{
	class Program
	{
		static void Main(string[] args)
		{
			int[,] array = DivisorArray.CriarArray(8, 8);

			DivisorArray.Print(array);
			Console.WriteLine();

			IEnumerable<int[,]> result = DivisorArray.Dividir(array);

			foreach (var a in result)
			{
				DivisorArray.Print(a);
				Console.WriteLine();
			}

			Console.ReadKey();
		}
	}
}
