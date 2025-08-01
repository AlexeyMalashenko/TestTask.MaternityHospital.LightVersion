using System;
using System.ComponentModel.DataAnnotations;

namespace TestTask.MaternityHospital.StorageService.Interfaces.Patients.Models;

public sealed class PatientDataModel
{
    [Key]
    public string PatientGlobalKey { get; set; }
    public string Use { get; set; }
    public string Family { get; set; }
    public string GivenFirst { get; set; }
    public string GivenSecond { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDateTimeUtc { get; set; }
    public bool IsActive { get; set; }
}