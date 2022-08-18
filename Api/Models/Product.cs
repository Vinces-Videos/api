using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Utilities;

namespace Models;

[DbCollectionName("Products")]
public class Product : DatabaseItem
{
    [BsonElement("title")]
    public string? Title { get; set; }

    [BsonElement("type")]
    public string? Type { get; set; }

    [BsonElement("ageRating")]
    public string? AgeRating { get; set; }
}