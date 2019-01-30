using System.Collections.ObjectModel;
using OpenMRS_Clone.Attributes;

namespace OpenMRS_Clone.Core
{
	public class BaseCustomizableMetadata<TAttribute> :
		BaseChangeableMetadata, ICustomizable<TAttribute>
		where TAttribute : IAttribute
	{
		public Collection<TAttribute> Attributes { get; }
		public Collection<TAttribute> ActiveAttributes { get; }

		public void AddAttribute(TAttribute attribute)
		{
			throw new System.NotImplementedException();
		}
	}
}