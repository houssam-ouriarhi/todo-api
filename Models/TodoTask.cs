using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyApi;

public class TodoTask
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Label { get; set; }

    public string? Description { get; set; }

    public override string ToString()
    {
        return "{label: " + Label + "; description: " + Description + "}";
    }
}