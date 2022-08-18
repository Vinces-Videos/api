using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Models;

[BsonIgnoreExtraElements]
//An item which can be stored within the DB
public class DatabaseItem
{
    //The object ID within the database, this must be settable for constructing the object from the DB.
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}