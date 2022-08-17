using Models;

public interface IDatabaseController
{
    //Get a full rowset from a collection
    public List<T> GetCollection<T>(string collectionName);

    //Get a database item by it's object id
    public T GetById<T>(string id, string collectionName) where T : DatabaseItem;
}