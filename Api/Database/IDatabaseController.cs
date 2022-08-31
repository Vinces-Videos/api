using Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Database;

public interface IDatabaseController
{
    //Ensures that the ID you're passing in is valid for the db service
    public bool IsValidId(string id);

    IMongoCollection<T> GetCollection<T>();

    IMongoQueryable<T> GetQueryableCollection<T>();

    //Get a full rowset from a collection
    public List<T> GetCollectionRows<T>();

    //Get a rowset from a collection by it's class type
    public List<T> GetCollectionByType<T>();

    //Get a database item by it's object id
    public T GetById<T>(string id) where T : DatabaseItem;

    //Get a database item by it's name, this expects a property of type name
    public List<T> GetByName<T>(string name) where T : IDatabaseNameable;

    public bool DeleteById<T>(string id) where T: DatabaseItem;

    public string Update<T>(T record) where T: DatabaseItem;

    public string Insert<T>(T record) where T: DatabaseItem;
}