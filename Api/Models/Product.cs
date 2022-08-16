using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Models;

[BsonIgnoreExtraElements]
public class Product
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("title")]
    public string Title { get; set; }
    [BsonElement("type")]
    public string Type { get; set; }
}