using System.Text.Json.Serialization;

namespace TestTask.MaternityHospital.PatientUploader.Domain.Models;

public sealed class Patient
{
    [JsonPropertyName("name")]
    public Name Name { get; set; } = new();
    [JsonPropertyName("gender")]
    public string? Gender { get; set; }
    [JsonPropertyName("birthDate")]
    public DateTime BirthDate { get; set; }
    [JsonPropertyName("active")]
    public bool Active { get; set; } 
}