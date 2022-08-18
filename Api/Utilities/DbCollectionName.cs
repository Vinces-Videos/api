//Allows us to bind a class to a collection name in the DB
public class DbCollectionName : Attribute
{
    private string name;

    public DbCollectionName(string _name)
    {
        name = _name;
    }
    
    public string GetName => name;
}