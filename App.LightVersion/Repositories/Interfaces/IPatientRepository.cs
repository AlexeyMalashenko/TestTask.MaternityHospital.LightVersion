using App.LightVersion.Models.Entities;

namespace App.LightVersion.Repositories.Interfaces;

public interface IPatientRepository
{
    Task SaveAsync(Patient patient);
    Task<Patient?> GetByIdAsync(string id);
}