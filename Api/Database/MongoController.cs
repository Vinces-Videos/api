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

    //Returns the list of available databases on the mongo client as a string.
    public string DatabaseListAsCVS() => string.Join(", ", _dbClient.ListDatabaseNames().ToList());

    //A collection is equivilant to a table, returns a full unfiltered table
    public List<T> GetCollection<T>() => _database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T))).Find(new BsonDocument()).ToList();

    //Get a single record by it's object Id from the 
    public T GetById<T>(string id) where T : DatabaseItem
    {
        var collection = _database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T))).AsQueryable();
        
        var result = collection.FirstOrDefault<T>(record => record.Id == id);
        if (result == null)
            throw new KeyNotFoundException();

        return result;
    }

    public bool IsValidId(string id) => ObjectId.TryParse(id, out ObjectId objectId);
}