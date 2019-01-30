using OpenMRS_Clone.Core;
using OpenMRS_Clone.Persons;

namespace OpenMRS_Clone.Relationships
{
	public class Relationship : BaseChangeableData
	{
		public Person PersonA { get; set; }
		public Person PersonB { get; set; }

		public RelationshipType RelationshipType { get; set; }
	}
}