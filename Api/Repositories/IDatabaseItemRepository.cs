namespace Repositories;

public interface IDatabaseItemRepository<T>
{
    T Get(string id);
    List<T> Get();
    void Delete(string id);
    T Put(T item);
}