namespace OpenMRS_Clone.Core
{
	public interface IMetadata : IAuditable, IRetireable
	{
		string Name { get; set; }
		string Description { get; set; }
	}
}