using GalaSoft.MvvmLight;
using Miotec.BioSinais.DSP;
using Miotec.BioSinais.GeradoresSinal;
using System.Collections.Generic;
using System.Linq;

namespace CalculaFFT
{
	public class MainViewModel : ViewModelBase
	{
		int _taxa = 2000;

		public MainViewModel()
		{
			System.Console.WriteLine("coiso");
		}

		public List<double> Sinal
		{
			get
			{
				if (_sinal == null)
				{
					var duração = 0.125;

					var info = new GeradorSinalInfo()
					{
						AmplitudeInferior = -1,
						AmplitudeSuperior = 1,
						Frequência = 5,
						Tipo = TipoSinalEnum.Senoidal,
						Taxa = _taxa
					};
					var gerador = new GeradorEMG(info);
					List<double> resultado = Enumerable.Range(0, (int)(duração * _taxa))
															  .Select(i => gerador.NextSample())
															  .ToList();

					_sinal = resultado;
				}
				return _sinal;
			}
		}
		List<double> _sinal = null;

		public IEnumerable<double> FFT
		{
			get
			{
				return new FFT(_sinal, _taxa).Magnitudes;
			}				
		}
	}
}
