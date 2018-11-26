using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelayTaskRun
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = Stopwatch.StartNew();

			Console.WriteLine(sw.ElapsedMilliseconds);
			Console.WriteLine(sw.ElapsedMilliseconds);

			Task.Run(() =>
			{
				Console.WriteLine(sw.ElapsedMilliseconds);
			});

			Console.ReadKey();
		}
	}
}
