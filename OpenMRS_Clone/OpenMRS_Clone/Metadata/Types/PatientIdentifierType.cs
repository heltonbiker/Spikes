using OpenMRS_Clone.Core;

namespace OpenMRS_Clone.Patients
{
	public class PatientIdentifierType : BaseChangeableMetadata
	{
		public int PatientIdentifierType_ { get; set; }
		public string Format { get; set; }
		public bool Required { get; set; }
		public string FormatDescription { get; set; }
		public string Validator { get; set; }


		public LocationBehavior LocationBehavior { get; set; }
		public UniquenessBehavior UniquenessBehavior { get; set; }
	}

	public enum UniquenessBehavior
	{
		UNIQUE,
		NON_UNIQUE,
		LOCATION
	}

	public enum LocationBehavior
	{
		REQUIRED,
		NOT_USED
	}
}