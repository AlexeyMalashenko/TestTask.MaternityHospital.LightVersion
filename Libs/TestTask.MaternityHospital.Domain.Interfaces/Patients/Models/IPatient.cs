using System;
using System.Threading.Tasks;

namespace App.LightVersion.Models.Entites;
public interface IPatient
{
    string PatientGlobalKey { get; }
    string Use { get; }
    string Family { get; set; }
    public string GivenFirst { get; }
    public string GivenSecond { get; }
    GenderType Gender { get; }
    DateTime BirthDateTimeUtc { get; set; }
    bool IsActive { get; }
    void Configure(string use,
        string givenFirst,
        string givenSecond,
        GenderType gender,
        bool isActive);
    Task Save();
    Task Delete();
}