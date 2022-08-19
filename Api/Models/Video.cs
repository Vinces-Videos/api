using MongoDB.Bson.Serialization.Attributes;
namespace Models;

public class Video : Product
{
    [BsonElement("durationMinutes")]
    public int DurationMinutes { get; set; }

    [BsonElement("ageRating")]
    public string? AgeRating { get; set; }

    [BsonElement("category")]
    public string Category { get; set; }

    public Video()
    {
        Type = "Video";
    }
}