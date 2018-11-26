using System;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace AppBase
{
	public class ApplicationBase : Application
	{
		static Version Version => Assembly.GetEntryAssembly().GetName().Version;
		static readonly string AppName = $"ForceViewer {Version.Major}.{Version.Minor}";

		static readonly Mutex mutex = new Mutex(true, AppName);


		[STAThread]
		public static void Main(string[] args)
		{
			try
			{
				var splashScreen = new SplashScreen("Splash.png");
				splashScreen.Show(true);

				if (mutex.WaitOne(TimeSpan.Zero, true))
				{
					//Iniciar();
					mutex.ReleaseMutex();
				}
				else
				{
					Extensions.EnviaMensagemPraAtivarOutraJanela();
				}
			}
			catch (Exception e)
			{
				//TratadorExcecaoGlobal.TratarException(e);
			}
		}

		//static void Iniciar()
		//{
		//	var app = new ApplicationBase();
		//	app.InitializeComponent();
		//	app.Run();
		//}


		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			//WpfLauncher.Launch(this, new Bootstrapper(AppName));
		}

	}
}