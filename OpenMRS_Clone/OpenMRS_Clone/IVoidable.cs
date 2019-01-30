using System;
using OpenMRS_Clone.Users;

namespace OpenMRS_Clone.Core
{
	public interface IVoidable
	{
		bool Voided { get; set; }

		User VoidedBy { get; set; }
		DateTime DateVoided { get; set; }
		string VoidReason { get; set; }
	}
}