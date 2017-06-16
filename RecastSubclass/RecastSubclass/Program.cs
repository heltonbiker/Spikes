using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecastSubclass
{
	class Program
	{
		static void Main(string[] args)
		{
			var obj = new SubClass() as BaseClass;

			Console.WriteLine(obj is ISubClass);

			Console.ReadKey();
		}
	}

	class BaseClass { }

	class SubClass : BaseClass, ISubClass { }

	interface ISubClass { }
}
