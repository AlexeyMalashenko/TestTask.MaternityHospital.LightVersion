using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestTask.MaternityHospital.StorageService.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PatientsDbContext>
{
    public PatientsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PatientsDbContext>();

        var connectionString = "Server=db;Port=3306;Database=db_maternity_hospital;Uid=admin;Pwd=admin;";

        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new PatientsDbContext(optionsBuilder.Options);
    }
}