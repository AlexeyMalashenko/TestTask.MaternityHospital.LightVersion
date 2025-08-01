using Microsoft.EntityFrameworkCore;
using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Models;

namespace TestTask.MaternityHospital.StorageService.Persistence
{
    public class PatientsDbContext : DbContext
    {
        public PatientsDbContext(DbContextOptions<PatientsDbContext> options) : base(options) { }

        public DbSet<PatientDataModel> Patients { get; set; }
    }
}
