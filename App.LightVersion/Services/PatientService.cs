using App.LightVersion.Models.Entities;
using App.LightVersion.Models.Enums;
using App.LightVersion.Repositories.Interfaces;
using App.LightVersion.Services.Interfaces;

namespace App.LightVersion.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;

    public PatientService(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Patient> CreatePatientAsync(Name name, DateTime birthDate, GenderType? gender, bool isActive)
    {
        var patient = new Patient(name, birthDate, gender, isActive);
        await _repository.SaveAsync(patient);
        return patient;
    }

    public async Task<Patient?> GetPatientByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }
}