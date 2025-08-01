using System;

namespace TestTask.MaternityHospital.Domain.Interfaces.Common.Exceptions;

public abstract class NotValidException : Exception
{
    protected NotValidException(string fieldName)
        : base($"{fieldName} cannot be null or empty")
    {
    }
    protected NotValidException(string fieldName, string errorMessage)
        : base(errorMessage)
    {
    }
}
