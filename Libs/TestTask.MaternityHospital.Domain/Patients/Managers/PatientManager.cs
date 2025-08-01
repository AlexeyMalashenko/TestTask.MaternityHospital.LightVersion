using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.MaternityHospital.Domain.Interfaces.Common.Exceptions;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Manages;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Models;
using TestTask.MaternityHospital.Domain.Patients.Models;
using TestTask.MaternityHospital.FHIR.Models.Implementations;
using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Providers;

namespace TestTask.MaternityHospital.Domain.Patients.Managers;
public sealed class PatientManager : IPatientManager
{
    private readonly IPatientDataProvider _patientDataProvider;
    public PatientManager(IPatientDataProvider patientDataProvider)
    {
        _patientDataProvider = patientDataProvider;
    }


    public IPatient CreatePatient(string family, DateTime birthDateTimeUtc)
    {
        var patient = new Patient(family, birthDateTimeUtc, _patientDataProvider);
        return patient;
    }

    public async Task<IPatient> GetPatient(string id)
    {
        var dataModel = await _patientDataProvider.GetPatient(id);

        if (dataModel == null)
        {
            throw new InstanceNotFoundException<IPatient>(nameof(id), id);
        }

        var patient = new Patient(dataModel, _patientDataProvider);
        return patient;
    }

    public async Task<IReadOnlyCollection<IPatient>> SearchPatientsByBirthDateAsync(List<BirthDateSearchFilter> filters)
    {
        var patientDataModels = await _patientDataProvider.SearchPatientsByBirthDate(filters);

        var patients = patientDataModels.Select(p => new Patient(p, _patientDataProvider)).ToList();

        return patients;
    }

}