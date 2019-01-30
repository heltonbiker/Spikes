using System;
using OpenMRS_Clone.Users;

namespace OpenMRS_Clone.Core
{
	public abstract class BaseMetadata : BaseObject, IMetadata
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public User Creator { get; set; }
		public DateTime DateCreated { get; set; }

		public User ChangedBy { get; set; }
		public DateTime DateChanged { get; set; }

		public bool Retired { get; set; }
		public User RetiredBy { get; set; }
		public DateTime DateRetired { get; set; }
		public string RetireReason { get; set; }
	}
}