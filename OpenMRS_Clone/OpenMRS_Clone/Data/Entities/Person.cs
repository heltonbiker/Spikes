using System.Collections.Generic;
using OpenMRS_Clone.Core;
using OpenMRS_Clone.Users;

namespace OpenMRS_Clone.Persons
{
	public abstract class Person : BaseChangeableData
	{
		protected int PersonId { get; set; }

		public HashSet<PersonAttribute> Attributes { get; set; }
	}
}