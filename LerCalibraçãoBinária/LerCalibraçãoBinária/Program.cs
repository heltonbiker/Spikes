using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerCalibraçãoBinária
{
	class Program
	{
		static void Main(string[] args)
		{
			var bytes = File.ReadAllBytes(@"E:\Google Drive\00 MIOTEC\01 PROJETOS\07 ForceView (Fukuda)\calibracao.bytes");
			var floats = new float[30];
			Buffer.BlockCopy(bytes, 0, floats, 0, bytes.Length);
			foreach (var f in floats)
			{
				Console.WriteLine(f);
			}
			Console.ReadKey();
		}
	}
}
