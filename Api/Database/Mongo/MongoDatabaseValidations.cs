using MongoDB.Bson;
using Database;

namespace Database.Mongo;

public class MongoDatabaseValidations : IDatabaseValidations
{
    public bool IsValidId(string id) => ObjectId.TryParse(id, out ObjectId objectId);
}