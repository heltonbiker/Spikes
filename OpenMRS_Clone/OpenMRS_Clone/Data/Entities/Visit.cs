using System.Collections.Generic;
using OpenMRS_Clone.Core;
using OpenMRS_Clone.Encounters;
using OpenMRS_Clone.Patients;

namespace OpenMRS_Clone.Visits
{
	public class Visit : BaseCustomizableData<VisitAttribute>
	{
		public int VisitId { get; set; }
		public Patient Patient { get; set; }
		public HashSet<Encounter> Encounters { get; set; }
		public VisitType VisitType { get; set; }
	}
}