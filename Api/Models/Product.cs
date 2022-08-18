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
    public string? Type { get; set; }

    [BsonElement("stockCount")]
    public int StockCount { get; set; }
}