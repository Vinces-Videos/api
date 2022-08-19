using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Models;

[BsonIgnoreExtraElements]
//An item which can be stored within the DB
public class DatabaseItem
{
    //The object ID within the database, this must be settable for constructing the object from the DB.
    //Internal objects are ignored by swagger
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    internal string Id { get; set; }

    //Whether or not the record is archived.
    [BsonElement("archived")]
    public bool Archived { get; set; } = false;

    public DatabaseItem()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
}