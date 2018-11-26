namespace OpenMRS_Clone.Patients
{
	class PatientIdentifierType
	{
		LocationBehavior locationBehavior;
		UniquenessBehavior uniquenessBehavior;
	}

	enum UniquenessBehavior
	{
		UNIQUE,
		NON_UNIQUE,
		LOCATION
	}

	enum LocationBehavior
	{
		REQUIRED,
		NOT_USED
	}
}