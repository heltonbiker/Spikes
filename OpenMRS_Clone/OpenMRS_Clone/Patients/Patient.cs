using System.Collections.Generic;
using OpenMRS_Clone.Persons;

namespace OpenMRS_Clone.Patients
{
	public class Patient : Person
	{
		HashSet<PatientIdentifier> _identifiers;
	}
}