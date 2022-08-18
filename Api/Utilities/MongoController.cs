using MongoDB.Driver;
using MongoDB.Bson;
using Models;
using Helpers;

namespace Utilities;

//Controllers can inherit from DBController to interact with the DB
public class MongoController : IDatabaseController
{
    //The mongo DB client
    private MongoClient DbClient { get; }
    //The database object
    private IMongoDatabase Database { get; }

    public MongoController()
    {
        //The connection string to the MongoDB Server
        var connectionString = ConfigurationManager.AppSetting["MongoDBConnectionString"]; 
        var dbName = ConfigurationManager.AppSetting["MongoDBName"]; 

        DbClient = new MongoClient(connectionString);
        Database = DbClient.GetDatabase(dbName);
    }

    //Returns the list of available databases on the mongo client as a string.
    public string DatabaseListAsCVS() => string.Join(", ", DbClient.ListDatabaseNames().ToList());

    //A collection is equivilant to a table, returns a full unfiltered table
    public List<T> GetCollection<T>() => Database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T))).Find(new BsonDocument()).ToList();

    //Get a single record by it's object Id from the 
    public T GetById<T>(string id) where T : DatabaseItem
    {
        var collection = Database.GetCollection<T>(AttributeHelper.GetDbCollectionName(typeof(T))).AsQueryable();
        
        var result = collection.FirstOrDefault<T>(record => record.Id == id);
        if (result == null)
            throw new KeyNotFoundException();

        return result;
    }

    public bool IsValidId(string id) => ObjectId.TryParse(id, out ObjectId objectId);
}