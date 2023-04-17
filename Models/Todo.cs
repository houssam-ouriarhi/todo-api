namespace MyApi;

public class Todo
{
    public uint? Id { get; set; }

    public string? Label { get; set; }

    public string? Description { get; set; }

    public override string ToString()
    {
        return "{label: " + Label + "; description: " + Description + "}";
    }
}