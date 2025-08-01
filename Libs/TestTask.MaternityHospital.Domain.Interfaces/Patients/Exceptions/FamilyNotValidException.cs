using TestTask.MaternityHospital.Domain.Interfaces.Common.Exceptions;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Models;

namespace TestTask.MaternityHospital.Domain.Interfaces.Patients.Exceptions;

public sealed class FamilyNotValidException : NotValidException
{
    public FamilyNotValidException()
        : base(nameof(IPatient.Family))
    {
    }
}
