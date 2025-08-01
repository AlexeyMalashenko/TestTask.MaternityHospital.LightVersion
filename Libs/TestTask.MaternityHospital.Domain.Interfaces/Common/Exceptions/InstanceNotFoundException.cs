using System;

namespace TestTask.MaternityHospital.Domain.Interfaces.Common.Exceptions;

public sealed class InstanceNotFoundException<T> : Exception
{
    public InstanceNotFoundException(string parameterName, string value)
        : base($"Instance of type [{typeof(T).Name}] not found by parameter {parameterName}[{value}]")
    {
    }
    public InstanceNotFoundException()
        : base($"Instance of type [{typeof(T).Name}] not found")
    {
    }
}