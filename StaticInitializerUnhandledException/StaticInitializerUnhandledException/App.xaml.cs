using System;
using System.Windows;
using System.Windows.Threading;

namespace TratamentoGlobalExcecao
{
	public partial class App : Application
	{
		[STAThread]
		static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException +=
				CurrentDomain_UnhandledException;

			var app = new App();
			app.InitializeComponent();
			app.Run(new MainWindow());
		}

		static App()
		{
			// Fazer alguma coisa aqui?
		}

		public App()
		{
			DispatcherUnhandledException +=
				App_DispatcherUnhandledException;
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			// Assinar algum evento aqui?
		}


		// EXCEPTION HANDLERS
		static void CurrentDomain_UnhandledException(
			object sender, UnhandledExceptionEventArgs e)
		{
			// Logar o que?
			// Fazer mais o que?
		}

		void App_DispatcherUnhandledException(
			object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			// Logar o que?
			// Fazer mais o que?
		}
	}
}