using Miotec.BioSinais.ModelosDeAplicação;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotec.BioSinais.ModeloDomínio;
using System.Reactive.Linq;
using System.Reactive;

namespace RxNovoFrame
{
	class Program
	{
		static void Main(string[] args)
		{
			ReceiverFake receiver = new ReceiverFake();

			var coiso = Observable.FromEventPattern<Frame<double>>(receiver, "NovoFrame");

			var sub = coiso.Subscribe(handle);

			receiver.Add(new Frame<double>(new List<double>()));
		}

		static Action<EventPattern<Frame<double>>> handle = (a) => { Console.WriteLine(a.EventArgs); };
	}

	internal class ReceiverFake : IRestoReceiver
	{
		public void Add(Frame<double> frame)
		{
			NovoFrame?.Invoke(this, frame);
		}


		public IEnumerable<ICanal> Canais
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool Conectado
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Nome
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int NívelBateria
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Plataforma Plataforma
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int SerialNumber
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int SubAmostragem
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public event EventHandler<Frame<double>> NovoFrame;
	}
}
