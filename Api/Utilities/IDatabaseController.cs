public interface IDatabaseController
{
    //Get a full rowset from a collection
    public List<DatabaseItem> GetCollection<DatabaseItem>(string collectionName);

    //Get a database item by it's object id
    public DatabaseItem GetById<DatabaseItem>(string id, string collectionName);
}