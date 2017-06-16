using Miotec.BaroPodometria;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaroSingleFrame
{
	class ViewModel
	{
		public ObservableCollection<CalculatedFrame> Footprints { get; set; }

		// CONSTRUTOR
		public ViewModel()
		{
			string path = @"E:\Google Drive\00 MIOTEC\01 PROJETOS\07 BaroPodometria\04 Coletas";

			Footprints = new ObservableCollection<CalculatedFrame>();

			foreach (var filename in Directory.EnumerateFiles(path, "*.txt", SearchOption.AllDirectories))
			{
				CalculatedFrame frame = DeserializeFrame(filename);
				Footprints.Add(frame);
				break;
			}

		}

		private CalculatedFrame DeserializeFrame(string filename)
		{
			var lines = File.ReadAllLines(filename);

			var linhas = 44;
			var colunas = 52;

			CalculatedFrame result = new CalculatedFrame(linhas, colunas);

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
					int linha = i / colunas;
					int coluna = i % colunas;
					result[linha, coluna] = Math.Max(result[linha,coluna], samples[i]);
				}
			}

			return result;
		}
	}
}
