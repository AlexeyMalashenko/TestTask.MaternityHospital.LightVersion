using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestTask.MaternityHospital.PatientUploader.Application.Interfaces;
using TestTask.MaternityHospital.PatientUploader.Configuration;
using TestTask.MaternityHospital.PatientUploader.Infrastructure;
using TestTask.MaternityHospital.PatientUploader.Infrastructure.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var apiSettings = context.Configuration.GetSection("ApiSettings").Get<ApiSettings>()!;
        services.AddSingleton(apiSettings);
        services.AddHttpClient<IPatientService, PatientService>();
    })
    .Build();

var settings = host.Services.GetRequiredService<ApiSettings>();
var patientService = host.Services.GetRequiredService<IPatientService>();

var patients = PatientGenerator.GenerateMany(settings.Count);
await patientService.UploadPatientsAsync(patients);
Console.WriteLine("Загрузка завершена");
