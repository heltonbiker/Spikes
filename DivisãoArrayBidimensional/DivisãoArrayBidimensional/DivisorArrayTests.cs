using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisãoArrayBidimensional
{
	[TestFixture]
	public class DivisorArrayTests
	{

		[TestCase(2,2)]
		[TestCase(3,2)]
		[TestCase(4,3)]
		[TestCase(5,3)]
		[TestCase(6,4)]
		public void TamanhoIdealFaixaTeste(int input, int expected)
		{
			int output = DivisorArray.TamanhoIdealFaixa(input);
			Assert.AreEqual(expected, output);
		}

		[TestCase(2,1)]
		[TestCase(3,2)]
		[TestCase(3,2)]
		[TestCase(4,2)]
		[TestCase(5,2)]
		public void FaixasFromTamanhoTeste(int input, int expected)
		{
			int output = DivisorArray.FaixasFromTamanho(input);
			Assert.AreEqual(expected, output);
		}

		[TestCase(1, 1, 0, 0)]
		[TestCase(2, 1, 0, 0)]
		[TestCase(2, 1, 1, 1)]
		//[TestCase()]
		//[TestCase()]
		//[TestCase()]
		public void TamanhoFaixaTeste(int tamanhoTotal, int tamanhoIdeal, int I, int expected)
		{
			int output = DivisorArray.TamanhoFaixa(I, tamanhoIdeal, tamanhoTotal);
			Assert.AreEqual(expected, output);
		}

		//[Test]
		//public void ArrayUnitário()
		//{
		//	var input = new int[2, 2]
		//	{
		//		{0,1},
		//		{2,3}
		//	};

		//	var result = DivisorArray.Dividir(input);

		//	CollectionAssert.AreEqual(result, new[] { input });
		//}

		//[Test]
		//public void Array_3x3()
		//{
		//	var input = new int[3, 3]
		//	{
		//		{0,1,2},
		//		{3,4,5},
		//		{6,7,8}
		//	};

		//	var result = DivisorArray.Dividir(input);

		//	var expected = new List<int[,]>()
		//	{
		//		new[,] {{0,1},{3,4}},
		//		new[,] {{3,4},{6,7}},
		//		new[,] {{1,2},{4,5}},
		//		new[,] {{4,5},{7,8}}
		//	};

		//	CollectionAssert.AreEqual(result, expected);
		//}

	}
}
