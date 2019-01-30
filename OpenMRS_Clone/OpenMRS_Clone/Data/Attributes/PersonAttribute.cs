using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Persons
{
	public class PersonAttribute : BaseChangeableData
	{
		public int PersonAttributeId { get; set; }
		public Person Person { get; set; }
		public PersonAttributeType AttributeType { get; set; }
		public string Value { get; set; }
	}
}