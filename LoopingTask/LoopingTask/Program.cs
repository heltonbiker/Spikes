using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoopingTask
{
	class Program
	{
		static void Main(string[] args)
		{
			Action método = new Action(() => Console.WriteLine("running"));

			TarefaEmLoop _loopingTask = new TarefaEmLoop(método, 200);

			_loopingTask.Run();
			Thread.Sleep(2000);

			_loopingTask.Cancel();
			Console.WriteLine("terminou");
		}
	}


	public class TarefaEmLoop
	{
		protected Action _método;
		protected int _intervalo_miliseconds;
		protected CancellationTokenSource _tokenSource;

		public TarefaEmLoop(Action método, int intervalo)
		{
			_método = método;
			_intervalo_miliseconds = intervalo;
		}

		public virtual void Run()
		{
			_tokenSource = new CancellationTokenSource();
			new Task(Loop, _tokenSource.Token, TaskCreationOptions.LongRunning)
				.Start();
		}

		public virtual void Cancel()
		{
			_tokenSource.Cancel();
			_tokenSource.Token.WaitHandle.WaitOne();
		}

		void Loop()
		{
			while (!_tokenSource.IsCancellationRequested)
			{
				_método();
				Thread.Sleep(_intervalo_miliseconds);
			}
		}
	}

}
