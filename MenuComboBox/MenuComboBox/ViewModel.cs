using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.ComponentModel;

namespace MenuComboBox
{
	public class ViewModel : ViewModelBase
	{
		public IEnumerable<string> Todos { get { return _todos; } }
		List<string> _todos = new List<string>()
		{
			"um",
			"dois",
			"três",
			"quatro",
			"cinco"
		};


		public IEnumerable<PontoColeta> PontosColeta { get { return _pontos_coleta; } }
		IEnumerable<PontoColeta> _pontos_coleta = new List<PontoColeta>()
		{
			new PontoColeta() {Nome = "pontoColeta um" },
			new PontoColeta() {Nome = "pontoColeta dois" },
			new PontoColeta() {Nome = "pontoColeta três" },
			new PontoColeta() {Nome = "pontoColeta quatro" },
			new PontoColeta() {Nome = "pontoColeta cinco" },
			new PontoColeta() {Nome = "pontoColeta seis" }
		};

		public IEnumerable<string> Disponíveis { get { return Todos; } }
	}

	public class PontoColeta : ObservableObject
	{
		public string Nome { get; set; }

		public string Selecionado
		{
			get { return _selecionado; }
			set
			{
				_selecionado = value;
				RaisePropertyChanged(() => Selecionado);
			}
		}
		string _selecionado = string.Empty;
	}
}
