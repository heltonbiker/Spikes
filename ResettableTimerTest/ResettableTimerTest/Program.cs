using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Subject<object> monitor = new Subject<object>();

			monitor.Timeout(TimeSpan.FromSeconds(1))
				   .Subscribe(obj => Console.WriteLine("reset"),
							  ex => Console.WriteLine("TIMEDOUT"));


			for (int small = 0; small < 30; small++)
			{
				Thread.Sleep(100);
				monitor.OnNext(new object());
			}



			Console.ReadKey();
		}
	}
}
