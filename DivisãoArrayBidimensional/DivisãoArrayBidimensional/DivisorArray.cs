using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisãoArrayBidimensional
{
	public static class DivisorArray
	{
		public static IEnumerable<T[,]> Dividir<T>(T[,] array)
		{
			throw new NotImplementedException();
		}

		public static IEnumerable<int[,]> Dividir(int[,] array)
		{
			int alturaIdealFaixa = TamanhoIdealFaixa(array.GetLength(0));
			int larguraIdealFaixa = TamanhoIdealFaixa(array.GetLength(1));

			int linhas = FaixasFromTamanho(array.GetLength(0));
			int colunas = FaixasFromTamanho(array.GetLength(1));


			// criar saída
			var final = new int[linhas, colunas][,];

			// criar recheio
			for (int I = 0; I < linhas; I++)
			{
				for (int J = 0; J < colunas; J++)
				{
					int alturaDestaFaixa = TamanhoFaixa(I, alturaIdealFaixa, array.GetLength(0));
					int larguraDestaFaixa = TamanhoFaixa(J, larguraIdealFaixa, array.GetLength(1));

					final[I, J] = new int[alturaDestaFaixa, larguraDestaFaixa];
				}
			}

			// preencher recheio
			for (int i = 0; i < linhas; i++)
			{
				for (int j = 0; j < colunas; j++)
				{
					int I = GetIndiceFaixa(i, alturaIdealFaixa);
					int J = GetIndiceFaixa(j, larguraIdealFaixa);

					int ii = GetIndiceElemento(i, alturaIdealFaixa);
					int jj = GetIndiceElemento(j, larguraIdealFaixa);

					final[I, J][ii, jj] = array[i, j];
				}
			}

			return final.Cast<int[,]>();
		}

		internal static int TamanhoIdealFaixa(int tamanhoArray)
		{
			return tamanhoArray / 2 + 1;
		}

		internal static int FaixasFromTamanho(int tamanhoArray)
		{
			return tamanhoArray > 2 ? 2 : 1;
		}


		static internal int TamanhoFaixa(int index, int tamanhoIdealFaixa, int tamanhoArrayOriginal)
		{
			var descontando = tamanhoArrayOriginal;

			for (int i = 0; i < index; i++)
			{
				descontando -= tamanhoIdealFaixa;
			}

			return descontando > tamanhoIdealFaixa ? tamanhoIdealFaixa
												   : descontando;
		}


		static int GetIndiceFaixa(int indiceArray, int tamanhoFaixa)
		{
			return indiceArray / tamanhoFaixa;
		}


		static int GetIndiceElemento(int indiceArray, int tamanhoFaixa)
		{
			return indiceArray % tamanhoFaixa;
		}



		public static int[,] CriarArray(int altura, int largura)
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

		public static void Print<T>(T[,] array)
		{
			var sb = new StringBuilder();

			foreach (var i in Enumerable.Range(0, array.GetLength(0)))
			{
				var vals = Enumerable.Range(0, array.GetLength(1))
									 .Select(j => array[i, j]);
				var row = String.Join("; ", vals.Select(v => v.ToString()));
				sb.AppendLine(row);
			}

			Console.WriteLine(sb.ToString());
		}

	}
}
