using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using Models;
using Helpers;
using Database;
using System.Collections.Generic;
using System.Linq;

namespace Database;

//Controllers can inherit from DBController to interact with the DB
public class DatabaseContextMock<T> : IDatabaseContext
{
    public List<T> MockData { get; set; }
    public DatabaseCollectionMock<DatabaseItem> DatabaseCollectionMock { get; set; }

    public bool DeleteById<T1>(string id) where T1 : DatabaseItem
    {
        throw new System.NotImplementedException();
    }

    public T1 GetById<T1>(string id) where T1 : DatabaseItem
    {
        throw new System.NotImplementedException();
    }

    public List<T1> GetByName<T1>(string name) where T1 : IDatabaseNameable
    {
        throw new System.NotImplementedException();
    }

    public IDatabaseCollection<DatabaseItem> GetCollection<T1>()
    {
        throw new System.NotImplementedException();
    }

    public List<T1> GetCollectionByType<T1>()
    {
        throw new System.NotImplementedException();
    }

    public List<T1> GetCollectionRows<T1>()
    {
        throw new System.NotImplementedException();
    }

    public IQueryable<T1> GetQueryableCollection<T1>()
    {
        throw new System.NotImplementedException();
    }

    public string Insert<T1>(T1 record) where T1 : DatabaseItem
    {
        throw new System.NotImplementedException();
    }

    public string Update<T1>(T1 record) where T1 : DatabaseItem
    {
        throw new System.NotImplementedException();
    }
    /*
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
   */
}