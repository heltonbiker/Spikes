using System;

namespace OpenMRS_Clone.Core
{
	public abstract class BaseObject : IObject
	{
		public int Id { get; set; }

		public string Uuid { get; set; } = Guid.NewGuid().ToString();

		// bool equals (object obj)
	}
}