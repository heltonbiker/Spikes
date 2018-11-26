using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplicaçãoMatrizesMathNumerics
{
	class Program
	{
		static void Main(string[] args)
		{
			var storage = Enumerable.Range(0, 10).Select(v => (double)v).ToArray();
			var entrada = Matrix<double>.Build.Dense(1, 10, storage);

			var coefstorage = Enumerable.Range(0, 30).Select(v => (double)v).ToArray();
			var coeficientes = Matrix<double>.Build.Dense(3, 10, coefstorage).Transpose();

			var product = entrada.Multiply(coeficientes);

			Console.WriteLine(coeficientes);
			Console.WriteLine(product);

			Console.ReadKey();
		}
	}
}
