using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CloseMainWindowMiotoolInput
{
	class Program
	{
		static void Main(string[] args)
		{
			string fname = @"E:\00 GIT\MiotecGit\BioGames.MiotoolInput\bin\Debug\Miotec.BioGames.MiotoolInput.exe";
			var processInfo = new ProcessStartInfo(fname);
			var process = Process.Start(processInfo);

			Thread.Sleep(3000);

			var result = process.CloseMainWindow();

			MessageBox.Show(result.ToString());
		}
	}
}
