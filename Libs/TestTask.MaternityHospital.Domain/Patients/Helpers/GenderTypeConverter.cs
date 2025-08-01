using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Enums;

namespace TestTask.MaternityHospital.Domain.Patients.Helpers;

internal static class GenderTypeConverter
{
    private const string female = "female";
    private const string male = "male";
    private const string other = "other";
    private const string unknown = "unknown";

    internal static GenderType Convert(string genderType)
    {
        switch (genderType.ToLower())
        {
            case female:
                return GenderType.Female;
            case male:
                return GenderType.Male;
            case other:
                return GenderType.Other;
            default:
                return GenderType.Unknown;
        }
    }

    internal static string Convert(GenderType genderType)
    {
        switch (genderType)
        {
            case GenderType.Female:
                return female;
            case GenderType.Male:
                return male;
            case GenderType.Other:
                return other;
            case GenderType.Unknown:
                return unknown;
            default:
                return unknown;
        }
    }
}