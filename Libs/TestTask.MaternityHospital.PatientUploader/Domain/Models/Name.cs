using System.Text.Json.Serialization;

namespace TestTask.MaternityHospital.PatientUploader.Domain.Models;

public class Name
{
    [JsonPropertyName("use")]
    public string Use { get; set; } = "official";
    [JsonPropertyName("family")]
    public string Family { get; set; } = string.Empty;
    [JsonPropertyName("given")]
    public List<string>? Given { get; set; } 
}