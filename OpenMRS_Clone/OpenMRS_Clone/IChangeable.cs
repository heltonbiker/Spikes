using System;
using OpenMRS_Clone.Users;

namespace OpenMRS_Clone.Core
{
	public interface IChangeable : IObject
	{
		User ChangedBy { get; set; }
		DateTime DateChanged { get; set; }
	}
}