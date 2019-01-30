using System.Collections.ObjectModel;
using OpenMRS_Clone.Attributes;

namespace OpenMRS_Clone.Core
{
	public interface ICustomizable { }

	public interface ICustomizable<TAttribute> : ICustomizable where TAttribute : IAttribute
	{
		Collection<TAttribute> Attributes { get; }

		Collection<TAttribute> ActiveAttributes { get; }

		void AddAttribute(TAttribute attribute);
	}
}