using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TestTask.MaternityHospital.WebApi.Contracts.Validators;

namespace TestTask.MaternityHospital.WebApi.Contracts.Patients.Incoming;

public sealed class PatientIncContract
{
    [Required]
    [JsonPropertyName("name")]
    public PatientNameIncContract Name { get; set; } = new();

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(BirthDateValidator), nameof(BirthDateValidator.Validate))]
    [JsonPropertyName("birthDate")]
    public DateTime BirthDate { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }
}