using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimelineScrubbing
{
	public interface IScrubbable
	{
		double Position { get; set; }
		double Speed { get; set; }

		bool Running { get; }

		void Stop();
		void Play();
		void Reverse();

		void Next();
		void Previous();
	}
}
