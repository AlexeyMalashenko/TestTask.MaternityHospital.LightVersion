using App.LightVersion.Models.Entities;
using App.LightVersion.Models.Enums;

namespace App.LightVersion.Services.Interfaces;

public interface IPatientService
{
    Task<Patient> CreatePatientAsync(Name name, DateTime birthDate, GenderType? gender, bool isActive);
    Task<Patient?> GetPatientByIdAsync(string id);
}
