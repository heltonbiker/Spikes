using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstancesFromListOfTypes
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Type> types = new List<Type> { typeof(MaxCalculation), typeof(AverageCalculation) };

			IEnumerable<Calculation> instances = types.Select(t => Activator.CreateInstance(t) as Calculation);
		}
	}

	abstract class Calculation { }

	class MaxCalculation : Calculation { }

	class AverageCalculation : Calculation { }
}
