using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Users
{
	public class User : BaseChangeableMetadata
	{
		public int UserId { get; set; }

		public HashSet<Role> Roles { get; set; }

		public Collection<Privilege> Privileges { get; set; }
	}
}