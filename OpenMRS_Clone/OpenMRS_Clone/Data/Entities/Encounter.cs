using System;
using System.Collections.Generic;
using OpenMRS_Clone.Core;
using OpenMRS_Clone.Providers;
using OpenMRS_Clone.Visits;

namespace OpenMRS_Clone.Encounters
{
	public class Encounter : BaseChangeableData
	{
		int encounterId;
		DateTime encounterDateTime;
		//patient
		//location
		//form
		EncounterType encounterType;
		Visit visit;
		HashSet<Provider> encounterProviders;
	}
}