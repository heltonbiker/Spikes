using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectShowLib;

namespace HelloDirectShowLib
{
	class Program
	{
		static void Main(string[] args)
		{
			var graphBuilder = (IGraphBuilder)(new FilterGraph());
			var captureGraphBuilder = (ICaptureGraphBuilder2)(new CaptureGraphBuilder2());
			var mediaControl = (IMediaControl)graphBuilder;
			var videoWindow = (IVideoWindow)graphBuilder;
			var mediaEventEx = (IMediaEventEx)graphBuilder;

			var x = 0;
		}
	}
}
