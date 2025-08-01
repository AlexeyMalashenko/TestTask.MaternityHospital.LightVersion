using TestTask.MaternityHospital.PatientUploader.Domain.Models;

namespace TestTask.MaternityHospital.PatientUploader.Application.Interfaces;

public interface IPatientService
{
    Task UploadPatientsAsync(IEnumerable<Patient> patients);
}