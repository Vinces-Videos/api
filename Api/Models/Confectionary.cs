using MongoDB.Bson.Serialization.Attributes;
namespace Models;

public class Confectionary : Product
{
    [BsonElement("calories")]
    public int Calories { get; set; }
    
    public Confectionary()
    {
        Type = "Confectionary";
    }
}