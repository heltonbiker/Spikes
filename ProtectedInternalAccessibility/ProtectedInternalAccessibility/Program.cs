using System;
using System.Linq;

namespace ProtectedInternalAccessibility
{
	class Program
	{
		static void Main(string[] args)
		{
			ConcreteChannel channel = new ConcreteChannel();
			ConcreteSensor sensor = new ConcreteSensor();

			channel.Sensor = sensor;

			sensor.WhenNewSamples.Subscribe(Console.WriteLine);

			channel.AddSamples(Enumerable.Range(0, 10).Select(Convert.ToDouble));
		}
	}
}
