using System;
using System.Collections.Generic;
using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Users
{
	public class Role : BaseChangeableMetadata
	{
		public string Role_ { get; set; }

		public HashSet<Privilege> Privileges { get; set; }
		public HashSet<Role> InheritedRoles { get; set; }
		public HashSet<Role> ChildRoles { get; set; }

		public void AddPrivilege(Privilege privilege)
		{
			throw new NotImplementedException();
		}

		public void RemovePrivilege(Privilege privilege)
		{
			throw new NotImplementedException();
		}

		public bool ContainsPrivilege(Privilege privilege)
		{
			throw new NotImplementedException();
		}
	}
}