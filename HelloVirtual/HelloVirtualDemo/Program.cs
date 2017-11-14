using HelloVirtual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloVirtualDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			var devices = new List<Ações>
			{
				new AçõesSimples(),
				new AçõesCompleto()
			};

			foreach (var device in devices)
			{
				Console.WriteLine("Para um dado device:");
				if (!device.Start())
					Console.WriteLine("Método Start não implementado");
				if (!device.Stop())
					Console.WriteLine("Método Stop não implementado");
			}
		}
	}
}
