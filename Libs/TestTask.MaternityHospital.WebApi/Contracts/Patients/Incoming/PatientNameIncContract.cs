using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestTask.MaternityHospital.WebApi.Contracts.Patients.Incoming;

public class PatientNameIncContract
{
    [JsonPropertyName("use")]
    public string Use { get; set; }

    [Required]
    [JsonPropertyName("family")]
    public string Family { get; set; }

    [JsonPropertyName("given")] 
    public List<string> Given { get; set; } = new();
}