using MongoDB.Bson.Serialization.Attributes;
using Database;

namespace Models;

[DbCollectionName("Products")]
[BsonIgnoreExtraElements]
public class Product : DatabaseItem
{
    [BsonElement("title")]
    public string? Title { get; set; }

    [BsonElement("type")]
    internal string? Type { get; set; }

    [BsonElement("stockCount")]
    public int StockCount { get; set; }

    [BsonElement("tags")]
    public Dictionary<string, string>? Tags { get; set ; }
}