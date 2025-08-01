using System.Collections.Generic;
using System.Text.Json.Serialization;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Models;

namespace TestTask.MaternityHospital.WebApi.Contracts.Patients.Outgoing;

public sealed class PatientNameOutContract
{
    [JsonPropertyName("id")]
    public string PatientGlobalKey { get; set; }
    [JsonPropertyName("use")]
    public string Use { get; set; }
    [JsonPropertyName("family")]
    public string Family { get; set; }
    [JsonPropertyName("given")]
    public List<string> Given { get; set; }

    public PatientNameOutContract(IPatient patient)
    {
        PatientGlobalKey = patient.PatientGlobalKey;
        Use = patient.Use;
        Family = patient.Family;
        Given = new List<string>
        {
            patient.GivenFirst,
            patient.GivenSecond
        };
    }
}