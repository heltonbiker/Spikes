using Stateless;
using System;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimelineScrubbing
{
	public class ScrubbingViewModel : ObservableObject
	{
		StateMachine<StateEnum, TriggerEnum> _stateMachine;

		const double NORMAL = 1;
		const double SLOW = 0.5;
		const double SUPER_SLOW = 0.2;

		const double TIME_STEP = 0.02;

		public double Duração { get; } = 12.345;


		public double Position
		{
			get { return _posição; }
			set
			{
				_posição = value;
				RaisePropertyChanged(null);
			}
		}
		double _posição = 0;


		public double Speed
		{
			get { return _speed; }
			set
			{
				_speed = value;
				RaisePropertyChanged(() => Speed);
			}
		}
		double _speed = 1;


		public bool Running => _stateMachine.State == StateEnum.Running;




		// CONSTRUTOR
		public ScrubbingViewModel()
		{
			ConfiguraStateMachine();
		}



		void ConfiguraStateMachine()
		{
			_stateMachine = new StateMachine<StateEnum, TriggerEnum>(StateEnum.Stopped);

			_stateMachine.OnTransitioned(t =>
			{
				RaisePropertyChanged(null);
			});


			_stateMachine.Configure(StateEnum.Global)
						 .Permit(TriggerEnum.Next, StateEnum.Stopped)
						 .Permit(TriggerEnum.Previous, StateEnum.Stopped)
						 .Permit(TriggerEnum.Play, StateEnum.Running)
						 .Permit(TriggerEnum.PlaySlow, StateEnum.Running)
						 .Permit(TriggerEnum.PlaySuperSlow, StateEnum.Running)
						 .Permit(TriggerEnum.Reverse, StateEnum.Running)
						 .Permit(TriggerEnum.ReverseSlow, StateEnum.Running)
						 .Permit(TriggerEnum.ReverseSuperSlow, StateEnum.Running);

			_stateMachine.Configure(StateEnum.Running)
						 .SubstateOf(StateEnum.Global)
						 .Permit(TriggerEnum.Stop, StateEnum.Stopped)
						 .OnEntryFrom(TriggerEnum.Play, () =>             Play(NORMAL))
						 .OnEntryFrom(TriggerEnum.PlaySlow, () =>         Play(SLOW))
						 .OnEntryFrom(TriggerEnum.PlaySuperSlow, () =>    Play(SUPER_SLOW))
						 .OnEntryFrom(TriggerEnum.Reverse, () =>          Play(-NORMAL))
						 .OnEntryFrom(TriggerEnum.ReverseSlow, () =>      Play(-SLOW))
						 .OnEntryFrom(TriggerEnum.ReverseSuperSlow, () => Play(-SUPER_SLOW));

			_stateMachine.Configure(StateEnum.Stopped).OnEntry(Stop)
						 .SubstateOf(StateEnum.Global)
						 .OnEntryFrom(TriggerEnum.Next, Next)
						 .OnEntryFrom(TriggerEnum.Previous, Previous);

		}






		public ICommand ComandoPlay => GetCommand(TriggerEnum.Play);
		public ICommand ComandoPlaySlow => GetCommand(TriggerEnum.PlaySlow);
		public ICommand ComandoPlaySuperSlow => GetCommand(TriggerEnum.PlaySuperSlow);

		public ICommand ComandoReverse => GetCommand(TriggerEnum.Reverse);
		public ICommand ComandoReverseSlow => GetCommand(TriggerEnum.ReverseSlow);
		public ICommand ComandoReverseSuperSlow => GetCommand(TriggerEnum.ReverseSuperSlow);

		public ICommand ComandoStop => GetCommand(TriggerEnum.Stop);

		public ICommand ComandoNext => GetCommand(TriggerEnum.Next);
		public ICommand ComandoPrevious => GetCommand(TriggerEnum.Previous);


		public ICommand ComandoPlayPause => Running ? ComandoStop
													: ComandoPlay;


		internal ICommand GetCommand(TriggerEnum trigger)
		{
			return new RelayCommand(param => _stateMachine.Fire(trigger),
									param => _stateMachine.CanFire(trigger));
		}





		CancellationTokenSource _cancelPlay;

		public void Play(double speed)
		{
			Speed = speed;			

			_cancelPlay?.Cancel();
			_cancelPlay = new CancellationTokenSource();
			Observable.Interval(TimeSpan.FromSeconds(TIME_STEP))
					  .Subscribe(t => ExecutaRun(), _cancelPlay.Token);
		}

		public void Stop()
		{
			_cancelPlay?.Cancel();
		}



		void ExecutaRun()
		{
			Task.Run(() =>
			{
				double incremento = Speed * TIME_STEP;
				double novaPosição = Position + incremento;

				if (novaPosição > Duração)
				{
					novaPosição = Duração;
					Stop();
				}
				if (novaPosição < 0)
				{
					novaPosição = 0;
					Stop();
				}

				Position = novaPosição;
			});
		}

		public void Next()
		{
			Print("chamou frame direito");
			Position = Math.Min(Position + TIME_STEP, Duração);
		}

		public void Previous()
		{
			Print("chamou frame esquerdo");
			Position = Math.Max(Position - TIME_STEP, 0);
		}









		public string Mensagens => sb.ToString();
		StringBuilder sb = new StringBuilder();

		void Print(object s)
		{
			sb.AppendLine(s.ToString());
			RaisePropertyChanged(() => Mensagens);
		}
	}


	public enum StateEnum
	{
		Global,
		Stopped,
		Running
	}

	public enum TriggerEnum
	{
		Play,
		PlaySlow,
		PlaySuperSlow,
		Reverse,
		ReverseSlow,
		ReverseSuperSlow,
		Stop,
		Next,
		Previous
	}

}
