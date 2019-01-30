using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Attributes
{
	public interface IAttributeType { }

	public interface IAttributeType<TOwner> : IAttributeType, IMetadata where TOwner : ICustomizable
	{
		
	}
}