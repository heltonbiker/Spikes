using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariableTimeLoop
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				var task1 = Task.Delay(500);
				var task2 = DoSomethingAsync();

				Task.WhenAll(task1, task2).Wait();
			}
		}

		static async Task DoSomethingAsync()
		{
			Random r = new Random();
			int miliseconds = Convert.ToInt32(r.NextDouble() * 1000);
			await Task.Delay(miliseconds);
			Console.WriteLine(miliseconds);
		}
	}
}
