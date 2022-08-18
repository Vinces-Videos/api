using Models;

namespace Database;

public interface IDatabaseController
{
    //Ensures that the ID you're passing in is valid for the db service
    public bool IsValidId(string id);

    //Get a full rowset from a collection
    public List<T> GetCollection<T>();

    //Get a database item by it's object id
    public T GetById<T>(string id) where T : DatabaseItem;

    public string Insert<T>(T record) where T: DatabaseItem;
}