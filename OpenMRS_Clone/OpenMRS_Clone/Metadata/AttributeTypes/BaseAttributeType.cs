using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Attributes
{
	public abstract class BaseAttributeType<TOwner> :
		BaseChangeableMetadata, IAttributeType<TOwner> 
		where TOwner : ICustomizable
	{
		
	}
}