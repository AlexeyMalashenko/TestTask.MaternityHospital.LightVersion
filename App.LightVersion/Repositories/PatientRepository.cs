using App.LightVersion.Data;
using App.LightVersion.Models.Entities;
using App.LightVersion.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.LightVersion.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly PatientsDbContext _dbContext;

    public PatientRepository(PatientsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task  SaveAsync(Patient patient)
    {
        var existing = await _dbContext.Patients.FindAsync(patient.Id);
        if (existing == null)
        {
            await _dbContext.Patients.AddAsync(patient);
        }
        else
        {
            _dbContext.Entry(existing).CurrentValues.SetValues(patient);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Patient?> GetByIdAsync(string id)
    {
        return await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
    }
}