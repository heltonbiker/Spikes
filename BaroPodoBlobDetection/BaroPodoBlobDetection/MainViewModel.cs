using GalaSoft.MvvmLight;
using Miotec.BaroPodo.ModeloDomínio;
using Miotec.PressureScan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaroPodoBlobDetection
{
	public class MainViewModel : ViewModelBase
	{
		public BaroFrame Frame { get; private set; }

		public IColorMap ColorMap => new MapaDeCorJet();

		public MainViewModel()
		{
			Frame = LoadLucas();
		}



		private BaroFrame LoadLucas()
		{
			var lines = File.ReadAllLines("lucasFootprint.txt");

			var linhas = 44;
			var colunas = 52;

			var result = new BaroFrame(linhas, colunas);

			foreach (var line in lines)
			{
				var values = line.Trim()
								 .Split(';')
								 .ToList();

				values.RemoveAt(values.Count - 1);

				var samples = values.Select(v => Convert.ToDouble(v))
									.ToArray(); ;

				for (int i = 0; i < samples.Length; i++)
				{
					if (samples[i] == 0)
						continue;


					int linha = i / colunas;
					int coluna = i % colunas;
					result[linha, coluna] = Math.Max(result[linha, coluna], samples[i] / 255);
				}
			}

			return result;

		}
	}
}
