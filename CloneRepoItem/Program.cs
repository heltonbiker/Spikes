using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CloneRepoItem
{

	[TestFixture]
	public class RepoItemTests
	{

		[Test]
		public void CloneHasSameId()
		{
			var one = new RepoItemApplication();
			var two = one.GetCopy();

			Assert.AreEqual(one.Id, two.Id);
		}


		[Test]
		public void CloneIsSubclassInstance()
		{
			var one = new ConcreteRepoItemAge();
			var two = one.GetCopy();

			Assert.IsInstanceOf<ConcreteRepoItemAge>(two);
		}


		[Test]
		public void ChangingCloneNameDoesntChangeOriginalAge()
		{
			var one = new ConcreteRepoItemName() { Name = "original" };
			var two = one.GetCopy() as ConcreteRepoItemName;
			Assert.AreEqual(one.Name, two.Name);

			two.Name = "modified";
			Assert.AreNotEqual(one.Name, two.Name);
		}

		[Test]
		public void ChangingCloneAgeDoesntChangeOriginalAge()
		{
			var one = new ConcreteRepoItemAge() { Age = 22 };
			var two = one.GetCopy() as ConcreteRepoItemAge;
			Assert.AreEqual(one.Age, two.Age);

			two.Age = 33;
			Assert.AreNotEqual(one.Age, two.Age);
		}
	}


	public class ConcreteRepoItemName : AbstractRepoItem<ConcreteRepoItemName>
	{
		public ConcreteRepoItemName() : base() { }

		public ConcreteRepoItemName(Guid id) : base(id) { }


		public string Name { get; set; }
	}


	public class RepoItemApplication
	{

	}

	[Serializable]
	public class RepoItemStorage
	{
		public string Name { get; set; }
	}


	public class ConcreteRepoItemAge : AbstractRepoItem<ConcreteRepoItemAge>
	{
		public ConcreteRepoItemAge() : base() { }

		// I don't want the constructor below to be public
		public ConcreteRepoItemAge(Guid id) : base(id) { }


		public decimal Age { get; set; }
	}


	public abstract class AbstractRepoItem<T> where T : AbstractRepoItem<T>, new()
	{
		public AbstractRepoItem()
		{
			Id = Guid.NewGuid();
		}

		// I don't want the constructor below to be public
		protected AbstractRepoItem(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; private set; }

		public T GetCopy()
		{
			//var clone = Activator.CreateInstance(typeof(T), new object[] { Id }) as T;

			///// HOW DO I COPY RUNTIME PROPERTIES HERE VIA REFLECTION?
			///// 
			//var propertyList = clone.GetType().GetProperties();
			//foreach (var property in propertyList)
			//{
			//	clone.GetCopy();
			//}

			//var clone = MemberwiseClone() as T;

			//return clone;

			var type = typeof(T);
			var clone = (T)FormatterServices.GetUninitializedObject(type);
			foreach (var propertyInfo in type.GetProperties())
			{
				var propval = propertyInfo.GetValue((T)this, null);
				propertyInfo.SetValue(clone, propval, null);
			}

			return clone;
		}
	}
}
