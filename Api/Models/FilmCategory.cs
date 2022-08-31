using Database;
using MongoDB.Bson.Serialization.Attributes;

namespace Models;

//Film category's have their own class as they are stored in the DB. This allows the front end to load categorys through the API rather than having to have a development request to add a new one
[DbCollectionName("FilmCategories")]
public class FilmCategory : DatabaseItem, IDatabaseNameable
{
    [BsonElement("name")]
    //The name value of a category
    public string Name { get; set; }

    [BsonElement("ratePerDay")]
    //The rate for renting per day
    public decimal RatePerDay { get; set; }

    public FilmCategory()
    {
        Type = "FilmCategory";
    }
}