using MongoDB.Driver;
using MongoDB.Bson;

namespace Utilities;

//Controllers can inherit from DBController to interact with the DB
public static class MongoController{
    //The mongo DB client
    private static MongoClient DbClient { get; }
    //The database object
    private static IMongoDatabase Database { get; }

    static MongoController()
    {
        //The connection string to the MongoDB Server
        var connectionString = ConfigurationManager.AppSetting["MongoDBConnectionString"]; 
        var dbName = ConfigurationManager.AppSetting["MongoDBName"]; 

        DbClient = new MongoClient(connectionString);
        Database = DbClient.GetDatabase(dbName);
    }

    //Returns the list of available databases on the mongo client as a string.
    public static string DatabaseListAsCVS() => string.Join(", ", DbClient.ListDatabaseNames().ToList());

    //A collection is equivilant to a table, returns a full unfiltered table
    public static List<T> GetCollection<T>(string name)
    {
        return Database.GetCollection<T>(name).Find(new BsonDocument()).ToList();
    }
}