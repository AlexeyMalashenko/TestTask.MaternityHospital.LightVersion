using System.Net.Http.Json;
using TestTask.MaternityHospital.PatientUploader.Application.Interfaces;
using TestTask.MaternityHospital.PatientUploader.Configuration;
using TestTask.MaternityHospital.PatientUploader.Domain.Models;

namespace TestTask.MaternityHospital.PatientUploader.Infrastructure.Services;

public class PatientService : IPatientService
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _settings;

    public PatientService(HttpClient httpClient, ApiSettings settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.BaseUrl);
        _settings = settings;
    }

    public async Task UploadPatientsAsync(IEnumerable<Patient> patients)
    {
        var response = await _httpClient.PostAsJsonAsync(_settings.PostPatientEndpoint, patients);
        response.EnsureSuccessStatusCode();

    }
}