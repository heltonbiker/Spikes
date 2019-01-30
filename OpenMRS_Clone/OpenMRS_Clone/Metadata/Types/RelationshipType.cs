using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Relationships
{
	public class RelationshipType : BaseChangeableMetadata
	{
		public int RelationshipTypeId { get; set; }

		string aIsToB;
		string bIsToA;

		int weight = 0;

		bool preferred = false;
	}
}