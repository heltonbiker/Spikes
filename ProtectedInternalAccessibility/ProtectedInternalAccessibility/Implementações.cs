using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ProtectedInternalAccessibility
{
	public class ConcreteChannel : Channel
	{
	}

	public class ConcreteSensor : Sensor
	{
		public override IObservable<double> WhenNewSamples
		{
			get { return _subject.AsObservable(); }
		}
		Subject<double> _subject = new Subject<double>();

		protected override void AddSamplesProtected(IEnumerable<double> samples)
		{
			samples.ToList().ForEach(sample => _subject.OnNext(sample));
		}
	}
}
