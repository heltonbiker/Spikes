using System;
using OpenMRS_Clone.Users;

namespace OpenMRS_Clone.Core
{
	public interface IRetireable
	{
		bool Retired { get; set; }

		User RetiredBy { get; set; }
		DateTime DateRetired { get; set; }
		string RetireReason { get; set; }

	}
}