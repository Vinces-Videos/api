using MongoDB.Bson.Serialization.Attributes;
namespace Models;

public class Invoice : Product
{
    //A list of rentals contained within this invoice
    [BsonElement("rental")]
    public List<string> Rental { get; set; }

    public Invoice()
    {
        Type = "Invoice";
    }
}