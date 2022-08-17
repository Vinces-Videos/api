using MongoDB.Driver;
using MongoDB.Bson;

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
    public List<DatabaseItem> GetCollection<DatabaseItem>(string collectionName) => Database.GetCollection<DatabaseItem>(collectionName).Find(new BsonDocument()).ToList();

    //Get a single record by it's object Id from the 
    public DatabaseItem GetById<DatabaseItem>(string id, string collectionName)
    {
        var collection = Database.GetCollection<DatabaseItem>(collectionName).AsQueryable<DatabaseItem>();
        var foo = collection.Where<DatabaseItem>(record => record.Id == "id");
        //var filter = Builders<DatabaseItem>.Filter.Eq("_id", id); //Builders allow adding multiple filters together
        //return collection.Find(filter).FirstOrDefault();
    }
}