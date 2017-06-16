using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TPLDataFlowTest
{
	class Program
	{
		static void Main(string[] args)
		{
			BlockToObservable();

			ObservableToBlock();

			Console.ReadKey();
		}

		private static void BlockToObservable()
		{
			var source = new BufferBlock<string>();
			var observable = source.AsObservable();
			observable.Subscribe(Console.WriteLine);

			source.Post("block to observable");
		}

		private static void ObservableToBlock()
		{
			var observable = new Subject<string>(); //Observable.Return("observable to block");
			var target = new ActionBlock<string>(s => Console.WriteLine(s));
			var observer = target.AsObserver();
			observable.Subscribe(observer);

			observable.OnNext("observable to block");
		}
	}
}