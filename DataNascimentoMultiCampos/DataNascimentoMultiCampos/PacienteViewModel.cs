using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNascimentoMultiCampos
{
	public class PacienteViewModel : ObservableObject
	{

		Paciente _paciente;

		// CONSTRUTOR
		public PacienteViewModel(Paciente paciente)
		{
			_paciente = paciente;
			DataNascimento = new DateTimeViewModel(paciente.DataNascimento);
		}

		public string Nome
		{
			get { return _nome; }
			set
			{
				_nome = value;
				RaisePropertyChanged(() => Nome);
			}
		}
		string _nome;



		public DateTimeViewModel DataNascimento
		{
			get { return _dataNascimento; }
			set
			{
				_dataNascimento = value;
				RaisePropertyChanged(() => DataNascimento);
			}
		}
		DateTimeViewModel _dataNascimento;


	}
}
