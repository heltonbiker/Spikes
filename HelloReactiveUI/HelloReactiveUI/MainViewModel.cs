using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloReactiveUI
{
	public class MainViewModel : ReactiveObject
	{
		public string PrimeiroNome
		{
			get { return _primeiroNome; }
			set { this.RaiseAndSetIfChanged(ref _primeiroNome, value); }
		}
		string _primeiroNome;

		public string SegundoNome
		{
			get { return _segundoNome; }
			set { this.RaiseAndSetIfChanged(ref _segundoNome, value); }
		}
		string _segundoNome;

		//public string NomeCompleto
		//{
		//	get { return _nomeCompleto; }
		//	set { this.RaiseAndSetIfChanged(ref _nomeCompleto, value); }
		//}
		//string _nomeCompleto;

		public string NomeCompleto
		{
			get { return _nomeCompleto.Value; }
		}
		readonly ObservableAsPropertyHelper<string> _nomeCompleto;


		//CONSTRUTOR
		public MainViewModel()
		{
			this.WhenAnyValue(x => x.PrimeiroNome, x => x.SegundoNome)
							.Where(x => !string.IsNullOrWhiteSpace(x.Item1) && !string.IsNullOrWhiteSpace(x.Item2))
							.Select(x => $"{x.Item1} de {x.Item2}")
							.Delay(TimeSpan.FromMilliseconds(500))
							.ToProperty(this, x => x.NomeCompleto, out _nomeCompleto);
		}
	}
}
