namespace App.LightVersion.Models.Entities;

public sealed class Name
{
    public string? Id { get; private set; }

    public string? Use { get; private set; }

    public string Family { get; private set; }

    public List<string> Given { get; private set; }

    private Name() { } // For EF

    public Name(string family, List<string> given, string? use = null, string? id = null)
    {
        if (string.IsNullOrWhiteSpace(family))
            throw new ArgumentException("Family name is required");

        Family = family;
        Given = given ?? new List<string>();
        Use = use;
        Id = id;
    }
}