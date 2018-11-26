using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Input;

namespace HelloDataflow
{
	public class MainViewModel : ViewModelBase
	{

		int xval = 0;
		int yval = 0;

		BufferBlock<double> XBlock;
		BufferBlock<double> YBlock;

		public ObservableCollection<double> Xs { get; set; }
			= new ObservableCollection<double>();

		public ObservableCollection<double> Ys { get; set; }
			= new ObservableCollection<double>();

		public ObservableCollection<Point> Pontos { get; set; }
			= new ObservableCollection<Point>();

		// CONSTRUTOR
		public MainViewModel()
		{
			XBlock = new BufferBlock<double>();
			YBlock = new BufferBlock<double>();

			var joinCoords = new JoinBlock<double, double>();
			
			var createPoints = new TransformBlock<Tuple<double, double>, Point>((p) => geraPonto(p));

			var addPoints = new ActionBlock<Point>((p) => 
				Application.Current.Dispatcher.Invoke(() => 
					Pontos.Add(p)
				)
			);

			XBlock.LinkTo(joinCoords.Target1);
			YBlock.LinkTo(joinCoords.Target2);

			joinCoords.LinkTo(createPoints);
			createPoints.LinkTo(addPoints);
		}

		Point geraPonto(Tuple<double, double> t)
		{
			//await Task.Delay(1000);
			return new Point(t.Item1, t.Item2);
		}

		public ICommand ComandoAdicionaX
			=> new RelayCommand(AdicionaX);
		void AdicionaX()
		{
			Xs.Add(xval);
			XBlock.SendAsync(xval++);
		}


		public ICommand ComandoAdicionaY
			=> new RelayCommand(AdicionaY);
		void AdicionaY()
		{
			Ys.Add(yval);
			YBlock.SendAsync(yval++);
		}

	}
}
