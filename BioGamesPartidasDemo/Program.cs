using Miotec.BioGames.Partidas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioGamesPartidasDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			string caminho = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "partida.json");

			var random = new Random();

			var frames = Enumerable.Range(0, 1000)
								   .Select(i => new Frame()
								   {
									   Amostras = Enumerable.Range(0, 8)
															.Select(j => Convert.ToSingle(random.NextDouble()))

								   })
								   .ToList();

			var protocolo = new Protocolo()
			{
				Atividades = Enumerable.Range(0, 6)
									  .Select(i => new Atividade(i, i * 10))
			};

			Partida partida = new Partida()
			{
				NomeJogo = "BioRock",
				Frames = frames,
				Protocolo = protocolo,
				TaxaAmostragem = 30
			};

			partida.Salvar(caminho);
		}
	}
}
