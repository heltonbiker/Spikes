using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Attributes
{
	public abstract class BaseAttribute<TAttribute, TOwner> 
		: BaseChangeableData, IAttribute<TAttribute, TOwner>
		where TAttribute : IAttributeType
		where TOwner : ICustomizable
	{
		public TOwner Owner { get; set; }

		public TAttribute Attribute { get; }
	}
}