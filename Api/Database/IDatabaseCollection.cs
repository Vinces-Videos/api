namespace Database;

public interface IDatabaseCollection<T>
{
    void DeleteOne(string id);
    void InsertOne(T record);
}