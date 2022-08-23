using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Models;

[BsonIgnoreExtraElements]
//An item which can be stored within the DB
public class DatabaseItem
{
    //The object ID within the database, this must be settable for constructing the object from the DB.
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    public string Id { get; set; }

    [BsonElement("type")]
    internal string? Type { get; set; }

    //Whether or not the record is archived.
    [BsonElement("archived")]
    public bool Archived { get; set; } = false;

    public DatabaseItem()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
}