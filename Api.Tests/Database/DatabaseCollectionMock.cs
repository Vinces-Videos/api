using System.Collections.Generic;
using System.Linq;
using Database;
using MongoDB.Driver;
using Models;

namespace Database.Mongo;

public class DatabaseCollectionMock<T> : IDatabaseCollection<T> where T : DatabaseItem
{
    private List<T> _collectionMock;

    public DatabaseCollectionMock(List<T> collectionMock)
    {
        _collectionMock = collectionMock;
    }

    public void DeleteOne(string id)
    {
        var recordToRemove = _collectionMock.FirstOrDefault(x => x.Id == id);
        if (recordToRemove == null)
        {
            return;
        }

        _collectionMock.Remove(recordToRemove);
    }

    public void InsertOne(T record)
    {
        _collectionMock.Add(record);
    }
}
