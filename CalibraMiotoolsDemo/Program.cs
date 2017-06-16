using Miotec.BioSinais.Hardware.MiotoolUSB;
using Miotec.BioSinais.Hardware.NewMiotool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalibraMiotoolsDemo
{
	class Program
	{
		static void Main(string[] args)
		{

			var calibranew = CalibraçãoEMGNewMiotool.CreateInstance();

			Console.WriteLine(calibranew.TransformBack(UInt16.MaxValue));
			Console.WriteLine(calibranew.TransformBack(UInt16.MinValue));



			var calibrausb = CalibraçãoEMGMiotoolUSB.CreateInstance();

			Console.WriteLine(calibrausb.TransformBack(Int32.MaxValue));
			Console.WriteLine(calibrausb.TransformBack(Int32.MinValue));
		}
	}
}
