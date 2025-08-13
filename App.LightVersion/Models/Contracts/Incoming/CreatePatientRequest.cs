namespace App.LightVersion.Models.Contracts.Incoming;

public class CreatePatientRequest
{
    public NameDto Name { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public bool Active { get; set; }

    public class NameDto
    {
        public string? Id { get; set; }
        public string? Use { get; set; }
        public string Family { get; set; } = null!;
        public List<string> Given { get; set; } = new();
    }
}