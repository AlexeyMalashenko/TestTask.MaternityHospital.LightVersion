using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestTask.MaternityHospital.WebApi.Contracts.Patients.Incoming;

public class PatientNameIncContract
{
    [JsonPropertyName("use")]
    public string Use { get; set; }
    [JsonPropertyName("family")]
    public string Family { get; set; }
    [JsonPropertyName("given")]
    public List<string> Given { get; set; }

    public PatientNameIncContract()
    {
        Given = new List<string>();
    }
}