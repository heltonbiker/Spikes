using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Patients
{
	public class PatientIdentifier : BaseChangeableData
	{
		public int PatientIdentifierId { get; set; }

		public string Identifier { get; set; }
		public Patient Patient { get; set; }
		public PatientIdentifierType IdentifierType { get; set; }
	}
}