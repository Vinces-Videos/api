using Models;

namespace Database;

public interface IDatabaseController
{
    //Get a full rowset from a collection
    public List<T> GetCollection<T>();

    //Get a database item by it's object id
    public T GetById<T>(string id) where T : DatabaseItem;

    //Ensures that the ID you're passing in is valid for the db service
    public bool IsValidId(string id);
}