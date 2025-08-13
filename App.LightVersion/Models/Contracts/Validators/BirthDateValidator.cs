using System;
using System.ComponentModel.DataAnnotations;

namespace App.LightVersion.Models.Contracts.Validators;

public static class BirthDateValidator
{
    public static ValidationResult Validate(DateTime date, ValidationContext context)
    {
        if (date == default)
            return new ValidationResult("BirthDate is required");

        if (date > DateTime.UtcNow)
            return new ValidationResult("BirthDate cannot be in the future");

        return ValidationResult.Success!;
    }
}