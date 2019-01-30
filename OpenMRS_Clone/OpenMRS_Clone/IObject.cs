using System;

namespace OpenMRS_Clone.Core
{
	public interface IObject
	{
		int Id { get; set; }
		string Uuid { get; set; }
	}
}