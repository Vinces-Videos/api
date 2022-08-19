using MongoDB.Driver;
using MongoDB.Bson;
using Models;
using Helpers;

namespace Database;

//Controllers can inherit from DBController to interact with the DB
public class MongoController : IDatabaseController
{
    //The mongo DB client
    private MongoClient _dbClient { get; }
    //The database object
    private IMongoDatabase _database { get; }

    public MongoController(IConfiguration configuration)
    {
        //The connection string to the MongoDB Server
        var connectionString = configuration["MongoDBConnectionString"];
        var dbName = configuration["MongoDBName"]; 

        _dbClient = new MongoClient(connectionString);
        _database = _dbClient.GetDatabase(dbName);
    }

    //Checks whether an ID can be parsed by MongoDB or not
    public bool IsValidId(string id) => ObjectId.TryParse(id, out ObjectId objectId);

    //Returns the list of available databases on the mongo client as a string.
    public string DatabaseListAsCVS() => string.Join(", ", _dbClient.ListDatabaseNames().ToList());

    //A collection is equivilant to a table, returns a full unfiltered table.
    public List<T> GetCollection<T>() => _database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T))).Find(_ => true).ToList();

    //A collection is equivilant to a table, returns a full unfiltered table.
    public List<T> GetCollectionByType<T>()
    {
        var collection = _database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T)));
        var filter = Builders<T>.Filter.Eq("type", typeof(T).Name);
        return collection.Find(filter).ToList();
    }

    //Get a single record by it's object Id from the database.
    public T GetById<T>(string id) where T : DatabaseItem
    {
        var collection = _database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T))).AsQueryable();
        
        var result = collection.FirstOrDefault<T>(x => x.Id == id);
        if (result == null)
            throw new KeyNotFoundException();

        return result;
    }

    //Get a single record by it's object Id from the database.
    public List<T> GetByName<T>(string name) where T : IDatabaseNameable
    {
        var collection = _database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T))).AsQueryable();
        
        var result = collection.Where<T>(x => x.Name == name).ToList();
        if (result == null)
            throw new KeyNotFoundException();

        return result;
    }

    ///Returns the record id.
    public string Insert<T>(T record) where T : DatabaseItem
    {
        //In this instance we get as a BsonDocument so we're able to insert as a BsonDocument too.
        var collection = _database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T)));
        collection.InsertOne(record);
        return record.Id;
    }   

    //Records should be archived in most cases instead of deleted
    public bool DeleteById<T>(string id) where T: DatabaseItem
    {
        //Do we need to do some cleanup on other objects that might reference this? The nature of NoSQL helps with this as they're not Id references
        var collection = _database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T)));
        return collection.DeleteOne(x => x.Id == id).DeletedCount > 0;
    }

    public string Update<T>(T record) where T: DatabaseItem
    {
        throw new NotImplementedException();
    }
}