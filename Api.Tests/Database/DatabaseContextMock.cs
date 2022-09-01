using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using Models;
using Helpers;
using Database;
using System.Collections.Generic;
using System.Linq;

namespace Database.Mongo;

//Controllers can inherit from DBController to interact with the DB
public class DatabaseContextMock : IDatabaseContext
{
    public bool DeleteById<T>(string id) where T : DatabaseItem
    {
        throw new System.NotImplementedException();
    }

    public T GetById<T>(string id) where T : DatabaseItem
    {
        throw new System.NotImplementedException();
    }

    public List<T> GetByName<T>(string name) where T : IDatabaseNameable
    {
        throw new System.NotImplementedException();
    }

    public IDatabaseCollection<T> GetCollection<T>()
    {
        throw new System.NotImplementedException();
    }

    public List<T> GetCollectionByType<T>()
    {
        throw new System.NotImplementedException();
    }

    public List<T> GetCollectionRows<T>()
    {
        throw new System.NotImplementedException();
    }

    public IQueryable<T> GetQueryableCollection<T>()
    {
        throw new System.NotImplementedException();
    }

    public string Insert<T>(T record) where T : DatabaseItem
    {
        throw new System.NotImplementedException();
    }

    public string Update<T>(T record) where T : DatabaseItem
    {
        throw new System.NotImplementedException();
    }
}