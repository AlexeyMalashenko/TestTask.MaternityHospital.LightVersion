using TestTask.MaternityHospital.FHIR.Enums;

namespace TestTask.MaternityHospital.FHIR.Models.Implementations;

public class BirthDateSearchFilter
{
    public FilterRule Operator { get; set; }
    public DateTime Date { get; set; }
    public BirthDateSearchFilter(FilterRule rule, DateTime date)
    {
        Operator = rule;
        Date = date;
    }
}