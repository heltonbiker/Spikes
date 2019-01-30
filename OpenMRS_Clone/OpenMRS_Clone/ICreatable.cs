using System;
using OpenMRS_Clone.Users;

namespace OpenMRS_Clone.Core
{
	public interface ICreatable
	{
		User Creator { get; set; }
		DateTime DateCreated { get; set; }
	}
}