using MongoDB.Driver;
using Models;

namespace Database.Mongo;

public class MongoDatabaseCollection<T> : IDatabaseCollection<T>
{
    private IMongoCollection<T> _mongoCollection;

    public MongoDatabaseCollection(IMongoCollection<T> mongoCollection)
    {
        _mongoCollection = mongoCollection;
    }

    public void DeleteOne(string id)
    {
        _mongoCollection.DeleteOne(id);
    }

    public void InsertOne(T record)
    {
        _mongoCollection.InsertOne(record);
    }
}
