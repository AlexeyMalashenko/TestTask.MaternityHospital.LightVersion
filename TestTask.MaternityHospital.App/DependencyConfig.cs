using Microsoft.Extensions.DependencyInjection;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Manages;
using TestTask.MaternityHospital.Domain.Patients.Managers;

namespace TestTask.MaternityHospital.App;

internal class DependencyConfig
{
    internal static void ConfigureServices(IServiceCollection services)
    {
        RegisterManager(services);
    }

    //private static void RegisterConfiguration(IServiceCollection services)
    //{
    //    services.AddHttpContextAccessor();
    //}

    private static void RegisterManager(IServiceCollection services)
    {
        services.AddScoped<IPatientManager, PatientManager>();
    }
}