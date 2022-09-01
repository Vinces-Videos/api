using MongoDB.Bson.Serialization.Attributes;
using Database;

namespace Models;

[DbCollectionName("Rentals")]
[BsonIgnoreExtraElements]
public class Rental : DatabaseItem, IDatabaseNameable
{
    [BsonElement("title")]
    public string Name { get; set; }

    [BsonElement("stockCount")]
    public int StockCount { get; set; }

    [BsonElement("tags")]
    public Dictionary<string, string>? Tags { get; set ; }
}