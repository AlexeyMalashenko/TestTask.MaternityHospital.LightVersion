using App.LightVersion.Models.Enums;

namespace App.LightVersion.Models.Entities;

public sealed class Patient
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();

    public Name Name { get; private set; }

    public DateTime BirthDate { get; private set; }

    public GenderType Gender { get; private set; }

    public bool IsActive { get; private set; }

    private Patient() { } // For EF

    public Patient(Name name, DateTime birthDate, GenderType? gender, bool isActive)
    {
        if (name == null || string.IsNullOrWhiteSpace(name.Family))
            throw new ArgumentException("Name with family is required.");

        if (birthDate == DateTime.MinValue)
            throw new ArgumentException("BirthDate is invalid.");

        Name = name;
        BirthDate = birthDate;
        Gender = gender ?? GenderType.Unknown;
        IsActive = isActive;
    }
}

