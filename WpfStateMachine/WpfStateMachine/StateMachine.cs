using Appccelerate.StateMachine;
using GalaSoft.MvvmLight;
using System;

namespace WpfStateMachine
{
    public class StateMachine : ViewModelBase
    {

        PassiveStateMachine<Estados, Eventos> _machine { get; set; }
            = new PassiveStateMachine<Estados, Eventos>();

		//CONSTRUTOR
		public StateMachine()
		{
			_machine.In(Estados.Parado)
					.On(Eventos.Play).Goto(Estados.Coletando);

			_machine.In(Estados.Coletando)
					.On(Eventos.Pause).Goto(Estados.Pausado)
					.On(Eventos.Stop).Goto(Estados.Parado);

			_machine.In(Estados.Pausado)
					.On(Eventos.Play).Goto(Estados.Coletando);

			_machine.In(Estados.Coletando)
					.ExecuteOnEntry(Start)
					.ExecuteOnExit(Stop);

			_machine.Start();
			_machine.Initialize(Estados.Parado);
		}


		private void Start()
		{
			throw new NotImplementedException();
		}

		private void Stop()
		{
			throw new NotImplementedException();
		}

	}



	enum Estados
    {
        Parado,
        Pausado,
        Coletando
    }

    enum Eventos
    {
        Play,
        Pause,
        Stop
    }
}
