using System;
using System.Text.Json.Serialization;

namespace TestTask.MaternityHospital.WebApi.Contracts.Patients.Incoming;

public sealed class PatientIncContract
{
    [JsonPropertyName("name")]
    public PatientNameIncContract Name { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    [JsonPropertyName("birthDate")]
    public DateTime BirthDate { get; set; }
    [JsonPropertyName("active")]
    public bool Active { get; set; }
    public PatientIncContract()
    {
        Name = new PatientNameIncContract();
    }
}