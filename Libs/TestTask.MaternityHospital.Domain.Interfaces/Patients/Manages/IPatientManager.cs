using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Models;
using TestTask.MaternityHospital.FHIR.Models.Implementations;

namespace TestTask.MaternityHospital.Domain.Interfaces.Patients.Manages;

public interface IPatientManager
{
    IPatient CreatePatient(string family, DateTime birthDateTimeUtc);
    Task<IPatient> GetPatient(string id);
    Task<IReadOnlyCollection<IPatient>> SearchPatientsByBirthDateAsync(List<BirthDateSearchFilter> filters);
}
