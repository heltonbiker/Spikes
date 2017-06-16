using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MenuTemplate
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public IEnumerable<TipoSinal> Ativos { get { return _ativos; } }
		IEnumerable<TipoSinal> _ativos = new List<TipoSinal>()
		{
			TipoSinal.EMG,
			TipoSinal.Força
		};

		public Dictionary<TipoSinal, List<OpçãoEscala>> OpçõesEscala { get; set; }
			= new Dictionary<TipoSinal, List<OpçãoEscala>>();



		// CONSTRUCTOR
		public MainWindow()
		{
			InitializeComponent();

			CarregaOpçõesDeEscala();

			DataContext = this;
		}



		void CarregaOpçõesDeEscala()
		{
			OpçõesEscala.Add(TipoSinal.Ângulo, new List<OpçãoEscala>()
			{
				new OpçãoEscala(45, TipoSinal.Ângulo),
				new OpçãoEscala(90, TipoSinal.Ângulo),
				new OpçãoEscala(180, TipoSinal.Ângulo),
				new OpçãoEscala(270, TipoSinal.Ângulo)
			});

			OpçõesEscala.Add(TipoSinal.EMG, new List<OpçãoEscala>()
			{
				new OpçãoEscala(10, TipoSinal.EMG),
				new OpçãoEscala(100, TipoSinal.EMG),
				new OpçãoEscala(500, TipoSinal.EMG),
				new OpçãoEscala(1000, TipoSinal.EMG),
				new OpçãoEscala(2000, TipoSinal.EMG),
				new OpçãoEscala(5000, TipoSinal.EMG),
				new OpçãoEscala(10000, TipoSinal.EMG),
				new OpçãoEscala(15000, TipoSinal.EMG)
			});

			OpçõesEscala.Add(TipoSinal.Força, new List<OpçãoEscala>()
			{
				new OpçãoEscala(1, TipoSinal.Força),
				new OpçãoEscala(2, TipoSinal.Força),
				new OpçãoEscala(5, TipoSinal.Força),
				new OpçãoEscala(10, TipoSinal.Força),
				new OpçãoEscala(20, TipoSinal.Força),
				new OpçãoEscala(50, TipoSinal.Força),
			});

		}



		public event PropertyChangedEventHandler PropertyChanged;

		void RaisePropertyChanged(string pname)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pname));
	}


	public class OpçãoEscala
	{
		public TipoSinal Tipo { get; private set; }

		public double Valor { get; private set; }

		/// REFACTOR: eliminar esta propriedade, transformando em MultiValueConverter na view
		/// Ou então fazer binding direto no objeto OpçãoEscala
		public string Rótulo
		{
			get
			{
				string unidade = string.Empty;

				switch (Tipo)
				{
					case TipoSinal.Acelerometria: unidade = " m/s²"; break;
					case TipoSinal.EMG: unidade = " μV"; break;
					case TipoSinal.Força: unidade = " kgF"; break;
					case TipoSinal.Ângulo: unidade = "°"; break;
				}

				return $"{Valor}{unidade}";
			}
		}

		public OpçãoEscala(double valor, TipoSinal tipo)
		{
			Valor = valor;
			Tipo = tipo;
		}
	}

	public enum TipoSinal : byte
	{
		Indefinido = 0,
		EMG = 1,
		Força = 2,
		Pressão = 3,
		Ângulo = 4,
		Acelerometria = 5,
		Indutivo = 6,
		Fluxo = 7,
		Isocinético = 8,
		Sincronismo = 9
	}

	public class VisibilidadeEscalaConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var Ativos = values[0] as IEnumerable<TipoSinal>;
			var Valor = (TipoSinal)values[1];

			Visibility resultado = Ativos.Contains(Valor) ? Visibility.Visible
														  : Visibility.Collapsed;

			return resultado;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
