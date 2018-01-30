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
			int[,] array = CriarArray(5, 7);

			var result = Dividir(array);

			Console.WriteLine(result);
		}

		static IEnumerable<int[,]> Dividir(int[,] array)
		{
			int alturaMáximaLinha = array.GetLength(0) / 2 + 1;
			int larguraMáximaColuna = array.GetLength(1) / 2 + 1;

			int linhas = array.GetLength(0) / alturaMáximaLinha + 1;
			int colunas = array.GetLength(1) / larguraMáximaColuna + 1;

			Console.WriteLine($"linhas {linhas}; colunas {colunas}");
			Console.ReadKey();

			// criar saída
			var final = new int[linhas, colunas][,];

			// criar recheio
			for (int I = 0; I < linhas; I++)
			{
				for (int J = 0; J < linhas; J++)
				{
					int altura = Altura(I);
					int largura = Largura(J);

					final[I, J] = new int[altura, largura];
				}
			}

			// preencher recheio
			for (int i = 0; i < linhas; i++)
			{
				for (int j = 0; j < colunas; j++)
				{
					int I = GetI(i,j);
					int J = GetJ(i,j);

					int ii = Get_ii(i, j);
					int jj = Get_jj(i, j);

					final[I, J][ii, jj] = array[i, j];
				}
			}

			return final.Cast<int[,]>();
		}



		static int Altura(int i)
		{
			throw new NotImplementedException();
		}

		static int Largura(int j)
		{
			throw new NotImplementedException();
		}



		static int GetI(int i, int j)
		{
			throw new NotImplementedException();
		}

		static int GetJ(int i, int j)
		{
			throw new NotImplementedException();
		}



		static int Get_ii(int i, int j)
		{
			throw new NotImplementedException();
		}

		static int Get_jj(int i, int j)
		{
			throw new NotImplementedException();
		}



		static int[,] CriarArray(int altura, int largura)
		{
			var result = new int[altura, largura];

			foreach (var i in Enumerable.Range(0, altura))
			{
				foreach (var j in Enumerable.Range(0, largura))
				{
					result[i, j] = i * largura + j;
				}
			}

			return result;
		}
	}
}
