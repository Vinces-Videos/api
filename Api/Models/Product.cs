using MongoDB.Bson.Serialization.Attributes;
using Database;

namespace Models;

[DbCollectionName("Products")]
[BsonIgnoreExtraElements]
public class Product : DatabaseItem, IDatabaseNameable
{
    [BsonElement("title")]
    public string Name { get; set; }

    [BsonElement("stockCount")]
    public int StockCount { get; set; }

    [BsonElement("tags")]
    public Dictionary<string, string>? Tags { get; set ; }
}