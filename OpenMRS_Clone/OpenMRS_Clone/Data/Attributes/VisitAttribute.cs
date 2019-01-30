using OpenMRS_Clone.Attributes;

namespace OpenMRS_Clone.Visits
{
	public class VisitAttribute : BaseAttribute<VisitAttributeType, Visit>
	{
		public int VisitAttributeId { get; set; }
	}
}