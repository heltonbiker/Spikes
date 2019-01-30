using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Attributes
{
	public interface IAttribute { }

	public interface IAttribute<out TAttributeType, TOwingType> : IAttribute
		where TAttributeType : IAttributeType 
		where TOwingType : ICustomizable
	{
		TOwingType Owner { get; set; }
		TAttributeType Attribute { get; }
	}
}