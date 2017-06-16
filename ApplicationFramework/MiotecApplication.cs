using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApplicationFramework
{
	public class MiotecApplication : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

			throw new Exception("Exceção automática hard-coded");
		}

		private void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
		{
			var ex = e.ExceptionObject as Exception;

			if (ex != null)
				MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);
		}
	}
}
