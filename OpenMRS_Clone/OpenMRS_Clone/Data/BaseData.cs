using System;
using OpenMRS_Clone.Users;

namespace OpenMRS_Clone.Core
{
	public class BaseData : BaseObject, IData
	{
		public User Creator { get; set; }
		public DateTime DateCreated { get; set; }

		public User ChangedBy { get; set; }
		public DateTime DateChanged { get; set; }

		public bool Voided { get; set; }
		public User VoidedBy { get; set; }
		public DateTime DateVoided { get; set; }
		public string VoidReason { get; set; }
	}
}