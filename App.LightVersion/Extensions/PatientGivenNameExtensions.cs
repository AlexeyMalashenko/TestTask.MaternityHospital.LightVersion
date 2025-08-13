#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace TestTask.MaternityHospital.WebApi.Extensions
{
    public static class PatientGivenNameExtensions
    {
        public static (string? First, string? Second) ExtractTwoGivenNames(this List<string>? given)
        {
            if (given == null || given.Count == 0)
                return (null, null);

            var first = given.ElementAtOrDefault(0);
            var second = given.ElementAtOrDefault(1);

            return (first, second);
        }
    }
}
