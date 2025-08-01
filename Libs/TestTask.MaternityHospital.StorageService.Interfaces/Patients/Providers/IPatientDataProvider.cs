using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.MaternityHospital.FHIR.Models.Implementations;
using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Models;

namespace TestTask.MaternityHospital.StorageService.Interfaces.Patients.Providers;

public interface IPatientDataProvider
{
    Task CreatePatient(PatientDataModel dataModel);
    Task<PatientDataModel> GetPatient(string patientGlobalKey);
    Task<List<PatientDataModel>> SearchPatientsByBirthDate(List<BirthDateSearchFilter> filters);
    Task UpdatePatient(PatientDataModel dataModel);
    Task DeletePatient(string patientGlobalKey);
}