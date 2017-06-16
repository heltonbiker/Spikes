using ApplicationFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApplicationFrameworkDemo
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			IBootstrapper bs = new BootstrapperDemo();			
			MiotecApplication app = new MiotecApplication();

			Window mainWindow = bs.GetBootstrappedWindow();

			app.Run(mainWindow);
		}
	}
}
