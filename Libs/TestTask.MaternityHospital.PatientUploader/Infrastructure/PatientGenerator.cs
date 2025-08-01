using TestTask.MaternityHospital.PatientUploader.Domain.Models;

namespace TestTask.MaternityHospital.PatientUploader.Infrastructure;

public static class PatientGenerator
{
    private static readonly string[] FirstNames = { "Иван", "Пётр", "Сергей", "Алексей", "Дмитрий" };
    private static readonly string[] SecondNames = { "Иванович", "Петрович", "Сергеевич", "Алексеевич" };
    private static readonly string[] LastNames = { "Иванов", "Петров", "Сидоров", "Смирнов" };
    private static readonly string[] Genders = { "male", "female", "other" };

    public static List<Patient> GenerateMany(int count)
    {
        var rnd = new Random();
        var result = new List<Patient>();

        for (int i = 0; i < count; i++)
        {
            var patient = new Patient
            {
                Name = new Name
                {
                    Family = LastNames[rnd.Next(LastNames.Length)],
                    Given = rnd.Next(0, 2) == 1
                        ? new List<string> { FirstNames[rnd.Next(FirstNames.Length)], SecondNames[rnd.Next(SecondNames.Length)] }
                        : null
                },
                BirthDate = DateTime.UtcNow.AddYears(-rnd.Next(1, 100)).AddDays(-rnd.Next(0, 365)),
                Gender = rnd.Next(0, 2) == 0 ? Genders[rnd.Next(Genders.Length)] : null,
                Active = rnd.Next(0, 2) != 0 && rnd.Next(0, 2) == 1
            };

            result.Add(patient);
        }

        return result;
    }
}