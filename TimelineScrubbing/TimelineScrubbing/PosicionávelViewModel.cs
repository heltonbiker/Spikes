using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Stateless;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimelineScrubbing
{
	public class PosicionávelViewModel : ObservableObject
	{
		//public double Duração => _duração.TotalSeconds;
		//TimeSpan _duração = TimeSpan.FromMilliseconds(12345);


		public double Posição
		{
			get { return _posição; }
			set
			{
				_posição = value;
				RaisePropertyChanged(null);
			}
		}
		double _posição = 0.4;

		//public double Tempo => _duração.TotalSeconds * Posição;



		StateMachine<StateEnum, TriggerEnum> _stateMachine;

		public bool Playing => _stateMachine.State == StateEnum.Playing;



		// CONSTRUTOR
		public PosicionávelViewModel()
		{			
			_stateMachine = new StateMachine<StateEnum, TriggerEnum>(StateEnum.Stopped);

			_stateMachine.OnTransitioned(t =>
			{
				RaisePropertyChanged(null);

				////CommandManager.InvalidateRequerySuggested();
				//foreach (var command in new ICommand[] { ComandoFrameDireito, ComandoFrameEsquerdo, ComandoPlayPause, ComandoStop })
				//{
				//	(command as RelayCommand)?.RaiseCanExecuteChanged();
				//}
			});

			_stateMachine.Configure(StateEnum.Global)
						 .Permit(TriggerEnum.FrameDireito, StateEnum.Paused)
						 .Permit(TriggerEnum.FrameEsquerdo, StateEnum.Paused);

			_stateMachine.Configure(StateEnum.Stopped).OnEntry(Stop)
						 //.SubstateOf(StateEnum.Global)
						 .Permit(TriggerEnum.PlayPause, StateEnum.Playing);

			_stateMachine.Configure(StateEnum.Playing).OnEntry(Play)
						 //.SubstateOf(StateEnum.Global)
						 .Permit(TriggerEnum.PlayPause, StateEnum.Paused)
						 .Permit(TriggerEnum.Stop, StateEnum.Stopped);

			_stateMachine.Configure(StateEnum.Paused).OnEntry(Pause)
						 //.SubstateOf(StateEnum.Global)
						 .Permit(TriggerEnum.PlayPause, StateEnum.Playing)
						 .Permit(TriggerEnum.Stop, StateEnum.Stopped)
						 .OnEntryFrom(TriggerEnum.FrameDireito, FrameDireito)
						 .OnEntryFrom(TriggerEnum.FrameEsquerdo, FrameEsquerdo);
		}




		void Play()
		{
			Print("chamou play");
		}

		void Pause()
		{
			Print("chamou pause");
		}

		void Stop()
		{
			Print("chamou stop");
		}

		void FrameDireito()
		{
			Print("chamou frame direito");
		}

		void FrameEsquerdo()
		{
			Print("chamou frame esquerdo");
		}



		public ICommand ComandoPlayPause => GetCommand(TriggerEnum.PlayPause);
		public ICommand ComandoStop => GetCommand(TriggerEnum.Stop);
		public ICommand ComandoFrameDireito => GetCommand(TriggerEnum.FrameDireito);
		public ICommand ComandoFrameEsquerdo => GetCommand(TriggerEnum.FrameEsquerdo);


		ICommand GetCommand(TriggerEnum trigger)
			=> new RelayCommand(() => Fire(trigger),
								() => _stateMachine.CanFire(trigger));

		void Fire(TriggerEnum trigger)
		{
			//Print(trigger);
			_stateMachine.Fire(trigger);
		}



		//public string Mensagens => sb.ToString();
		//StringBuilder sb = new StringBuilder();

		void Print(object s)
		{
			//Task.Run(() =>
			//{
			//	sb.AppendLine(s.ToString());
			//	RaisePropertyChanged(() => Mensagens);
			//});

			Console.WriteLine(s.ToString());
		}

	}

	public enum StateEnum
	{
		Global,
		Stopped,
		Playing,
		Paused	
	}

	public enum TriggerEnum
	{
		PlayPause,
		Stop,
		FrameDireito,
		FrameEsquerdo
	}
}
