using Miotec.BioSinais.DSP.Frequencia;
using Miotec.BioSinais.ExameService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RetificaSuaviza
{
	class Program
	{
		static void Main(string[] args)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			var filtro = new FiltroPassaBaixaIIR(10, 2000);

			var exame = new ExameSerializer(@"e:\trinta segundos.mio").Deserialize();

			List<Double> canalUm = exame.Trechos.ElementAt(0).Sinais[0].Amostras;


			double média = canalUm.Average();

			var corrigido = canalUm.Select(v => v - média).ToList();
			File.WriteAllLines(@"e:\RetificadoFiltrado\corrigido.txt",
							   corrigido.Select(v => v.ToString()));

			var retificado = corrigido.Select(v => Math.Abs(v));
			File.WriteAllLines(@"e:\RetificadoFiltrado\retificado.txt",
				               retificado.Select(v => v.ToString()));

			int filtersize = 100;
			var filtrado = retificado.Concat(Enumerable.Repeat<double>(0, filtersize))
									 .Select(v => filtro.Filtrar(v))
									 .Skip(filtersize);
			File.WriteAllLines(@"e:\RetificadoFiltrado\filtrado.txt",
							   filtrado.Select(v => v.ToString()));

		}
	}
}
