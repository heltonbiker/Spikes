using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloVirtual
{
	public abstract class Ações
	{
		public virtual bool Start() => false;
		public virtual bool Stop() => false;

		protected bool ConfirmMessage(string message)
		{
			Console.WriteLine(message);
			return true;
		}
	}

	public class AçõesSimples : Ações
	{
		public override bool Start()
		{
			return ConfirmMessage("StartSimples");
		}
	}

	public class AçõesCompleto : Ações
	{
		public override bool Start()
		{
			return ConfirmMessage("Start Completo");
		}

		public override bool Stop()
		{
			return ConfirmMessage("Stop Completo");
		}
	}
}
