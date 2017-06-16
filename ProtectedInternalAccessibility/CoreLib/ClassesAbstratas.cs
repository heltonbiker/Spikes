using System;
using System.Collections.Generic;

namespace CoreLib
{
	public abstract class Channel
	{
		public Sensor Sensor { get; set; }

		public void AddSamples(IEnumerable<double> samples)
		{
			Sensor?.AddSamplesInternal(samples);
		}
	}

	public abstract class Sensor
	{
		public abstract IObservable<double> WhenNewSamples { get; }

		internal void AddSamplesInternal(IEnumerable<double> samples)
		{
			AddSamplesProtected(samples);
		}

		protected abstract void AddSamplesProtected(IEnumerable<double> samples);
	}
}
