using System;
using System.Text.Json.Serialization;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Models;
using TestTask.MaternityHospital.WebApi.Helpers;

namespace TestTask.MaternityHospital.WebApi.Contracts.Patients.Outgoing;

public sealed class PatientOutContract
{
    [JsonPropertyName("name")]
    public PatientNameOutContract Name { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    [JsonPropertyName("birthDate")]
    public DateTime BirthDate { get; set; }
    [JsonPropertyName("active")]
    public bool Active { get; set; }
    public PatientOutContract(IPatient patient)
    {
        Name = new PatientNameOutContract(patient);
        Gender = GenderTypeConverter.Convert(patient.Gender);
        BirthDate = patient.BirthDateTimeUtc;
        Active = patient.IsActive;
    }
}