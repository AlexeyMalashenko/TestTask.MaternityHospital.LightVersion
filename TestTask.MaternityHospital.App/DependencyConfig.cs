using Microsoft.Extensions.DependencyInjection;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Manages;
using TestTask.MaternityHospital.Domain.Patients.Managers;
using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Providers;
using TestTask.MaternityHospital.StorageService.Patients.Providers;

namespace TestTask.MaternityHospital.App;

internal class DependencyConfig
{
    internal static void ConfigureServices(IServiceCollection services)
    {
        RegisterConfiguration(services);
        RegisterManager(services);
        RegisterProviders(services);
    }

    private static void RegisterConfiguration(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
    }

    private static void RegisterManager(IServiceCollection services)
    {
        services.AddScoped<IPatientManager, PatientManager>();
    }

    private static void RegisterProviders(IServiceCollection services)
    {
        services.AddScoped<IPatientDataProvider, PatientDataProvider>();
    }
}