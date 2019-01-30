using OpenMRS_Clone.Core;
using OpenMRS_Clone.Users;

namespace OpenMRS_Clone.Persons
{
	public class PersonAttributeType : BaseChangeableMetadata
	{
		public int PersonAttributeTypeId { get; set; }

		public string Format { get; set; }
		public int ForeignKey { get; set; }
		public double SortWeight { get; set; }
		public bool Searchable { get; set; }
		public Privilege EditPrivilege { get; set; }
	}
}