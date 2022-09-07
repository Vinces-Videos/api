using MongoDB.Driver;
using Models;

namespace Database.Mongo;

public class MongoDatabaseCollection<T> : IDatabaseCollection<T> where T : DatabaseItem
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
        //Check whether a matching record already exists, if it does replace rather than insert
        _mongoCollection.ReplaceOne(
            filter: (rec => rec.Id == record.Id),
            options: new ReplaceOptions { IsUpsert = true },
            replacement: record
        );
    }
}
