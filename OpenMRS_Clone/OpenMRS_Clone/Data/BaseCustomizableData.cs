using OpenMRS_Clone.Attributes;
using System.Collections.ObjectModel;

namespace OpenMRS_Clone.Core
{
	public abstract class BaseCustomizableData<TAttribute> :
		BaseChangeableData, ICustomizable<TAttribute> 
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