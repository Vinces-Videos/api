using Microsoft.Extensions.Caching.Memory;
using Models;
using Database;

namespace Repositories;

public class DatabaseItemRepository<T> : IDatabaseItemRepository<T> where T : DatabaseItem
{
    protected IMemoryCache _cache;
    protected IQueryable<T> _queryable;
    protected IDatabaseCollection<T> _collection;

    public DatabaseItemRepository(IDatabaseContext dbController, MemoryCacheOptions options)
    {
        _cache = new MemoryCache(options);
        _queryable = dbController.GetQueryableCollection<T>();
        _collection = dbController.GetCollection<T>();
    }

    public T Get(string id)
    {
        return _cache.GetOrCreate(id, cacheEntry => {
            cacheEntry.Size = 1;
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

            var result = _queryable.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Failed to find {typeof(T)} with id: {id}");
            }

            return result;
        });
    }

    public T Get(string id, bool bipassCache)
    {
        var result = _queryable.FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            throw new KeyNotFoundException($"Failed to find {typeof(T)} with id: {id}");
        }

        _cache.Set(id, result, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });

        return result;
    }

    //Updates or inserts
    public T Put(T item)
    {
        _collection.InsertOne(item);

        _cache.Set(item.Id, item, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });
        
        return item;
    }

    public List<T> Get()
    {
        //Some tables contain mixed classes of data, we must ensure that we only return data that's relevent at this point.
        return _queryable.Where(x => x.Type == typeof(T).Name).ToList();
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }
}