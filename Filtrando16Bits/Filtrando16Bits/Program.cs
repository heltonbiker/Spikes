using Miotec.BioSinais.DSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtrando16Bits
{
	class Program
	{
		static void Main(string[] args)
		{
			var filtro = new FiltroPassaBandaIIR(20, 500, 2000);

			var sinal = Enumerable.Range(0, 1000)
								  .Select(v =>
									  Convert.ToInt16(Math.Sin(v*0.05) * 100)
								  )
								  .ToList();

			foreach (var s in sinal)
				Console.WriteLine(s);
		}
	}
}
