using System.Collections.Generic;
using OpenMRS_Clone.Users;

namespace OpenMRS_Clone.Persons
{
	public class Person
	{
		protected int PersonId;

		HashSet<PersonAddress> _addresses;

		HashSet<PersonName> _names;

		HashSet<PersonAttribute> _attributes;


		User _personCreator;
	}
}