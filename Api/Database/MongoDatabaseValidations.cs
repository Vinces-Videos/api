using MongoDB.Bson;

namespace Database;

public class MongoDatabaseValidations : IDatabaseValidations
{
    public bool IsValidId(string id) => ObjectId.TryParse(id, out ObjectId objectId);
}