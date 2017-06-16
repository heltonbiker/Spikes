using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace UnzipRx
{
	class Program
	{
		static void Main(string[] args)
		{
			var source = new Subject<int>();

			int count = 3;

			var sources = new IObservable<int>[count];
			for (int i = 0; i < count; i++)
			{
				var enclosedI = i;
				sources[enclosedI] = source
					.Where((t, j) => j % count == enclosedI);
			}

			sources[0].Subscribe(Console.WriteLine);


			Console.ReadKey();
		}
	}
}
