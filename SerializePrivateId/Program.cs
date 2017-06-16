using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializePrivateId
{
	class Program
	{
		static void Main(string[] args)
		{
			var item = new RepoItem();
			item.Nome = "original";
			var outro = item.CriarCópia();

			Console.WriteLine(outro.Nome == "original");

			outro.Nome = "modificado";

			Console.WriteLine(outro.Id == item.Id);
			Console.WriteLine(outro.Nome != item.Nome);

			Console.ReadKey();
		}
	}

	public class RepoItem
	{
		public string Nome { get; set; }

		public Guid Id { get { return _id; } }
		readonly Guid _id;

		// CONSTRUTORES
		public RepoItem()
		{
			_id = Guid.NewGuid();
		}

		private RepoItem(Guid id)
		{
			_id = id;
		}

		public RepoItem CriarCópia()
		{
			return new RepoItem(_id)
			{
				Nome = Nome
			};
		}
	}
}
