using System.Collections.Generic;
using OpenMRS_Clone.Persons;

namespace OpenMRS_Clone.Patients
{
	public class Patient : Person
	{
		public int PatientId { get; set; }

		public HashSet<PatientIdentifier> Identifiers { get; set; }

		public Person Person => this;
	}
}