using System;
using System.Collections.Generic;
using OpenMRS_Clone.Patients;

namespace OpenMRS_Clone
{
	public interface IPatientService : IService
	{
		Patient SavePatient(Patient patient);
		Patient GetPatient(int patientId);
		Patient GetPatientOrPromotePerson(int patientOrPersonId);
		Patient GetPatientByUuid(string uuid);
		Patient VoidPatient(Patient patient, string reason);
		Patient UnvoidPatient(Patient patient);

		PatientIdentifier GetPatientIdentifierByUuid(string uuid);

		List<Patient> GetAllPatients();
		List<Patient> GetAllPatients(bool includeVoided);

		List<Patient> GetPatients(string name, string identifier, 
			List<PatientIdentifierType> identifierTypes,
			bool matchIdentifierExactly);

		List<Patient> GetPatients(string query);
		List<Patient> GetPatients(string query, int start, int length);
		List<Patient> GetPatients(string query, bool includeVoided, int start, int length);

		List<Patient> GetDuplicatePatientsByAttributes(List<string> attributes);

		Patient GetPatientByExample(Patient patientToMatch);


		List<PatientIdentifier> GetPatientIdentifiers(string identifier,
			List<PatientIdentifierType> patientIdentifierTypes,
			List<Patient> patients, bool isPreferred);

		PatientIdentifierType SavePatientIdentifierType(PatientIdentifierType patientIdentifierType);
		PatientIdentifierType GetPatientIdentifierType(int patientIdentifierTypeId);
		PatientIdentifierType GetPatientIdentifierTypeByUuid(string uuid);
		PatientIdentifierType GetPatientIdentifierTypeByName(string name);
		PatientIdentifierType RetirePatientIdentifierType(PatientIdentifierType patientIdentifierType, string reason);
		PatientIdentifierType UnretirePatientIdentifierType(PatientIdentifierType patientIdentifierType);

		List<PatientIdentifierType> GetAllPatientIdentifierTypes();
		List<PatientIdentifierType> GetAllPatientIdentifierTypes(bool includeRetired);
		List<PatientIdentifierType> GetPatientIdentifierTypes(string name, 
			string format, bool required,
			bool hasCheckDigit);

		void CheckPatientIdentifiers(Patient patient);
		void MergePatients(Patient preferred, Patient notPreferred);
		void MergePatients(Patient preferred, List<Patient> notPreferred);
	}
}