using ApplicationFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApplicationFrameworkDemo
{
	public class BootstrapperDemo : IBootstrapper
	{
		public Window GetBootstrappedWindow()
		{
			var result = new DemoWindow();

			return result;
		}
	}
}
