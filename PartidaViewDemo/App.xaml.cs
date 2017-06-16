using Miotec.BioGames.Partidas;
using Miotec.BioMovi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PartidaViewDemo
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			MainWindow = GetBootstrappedWindow();

			MainWindow.Show();
		}

		private Window GetBootstrappedWindow()
		{
			//var partida = new Partida()
			//{
			//	Frames = getFrames(),
			//	NomeJogo = "BioCrack",
			//	Protocolo = getProtocolo(),
			//	TaxaAmostragem = 5
			//};

			Partida partida = CarregarPartida();

			var w = new TelaAnáliseJogo();
			w.DataContext = new PartidaViewModel(partida);

			return w;
		}

		private Partida CarregarPartida()
		{
			var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			var path = Path.Combine(desktop, "BioGame", "Kinect", "partida.json");
			var result = Partida.Carregar(path);
			return result;
		}

		private IEnumerable<Frame> getFrames()
		{
			var frames = Enumerable.Range(0, 100)
								   .Select(i => Math.Max(0, -Math.Cos(i*0.2)) * 10)
								   .Select(v => new Frame()
									{
										Amostras = new[] { Convert.ToSingle(v) }
									});
			return frames;
		}

		private Protocolo getProtocolo()
		{
			var protocolo = new Protocolo();

			var atividades = new List<Atividade>();
			for (int i = 0; i < 10; i++)
			{
				atividades.Add(new Atividade(TimeSpan.FromSeconds(1), 0));
				atividades.Add(new Atividade(TimeSpan.FromSeconds(1), 0.2f));
			}

			protocolo.Atividades = atividades;

			return protocolo;
		}
	}
}
