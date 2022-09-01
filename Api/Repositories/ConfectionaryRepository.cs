using Microsoft.Extensions.Caching.Memory;
using Models;
using Database;

namespace Repositories;

public class ConfectionaryRepository : IConfectionaryRepository
{
    private IMemoryCache _cache;
    private IQueryable<Confectionary> _queryable;
    private IDatabaseCollection<Confectionary> _collection;

    public ConfectionaryRepository(IDatabaseContext dbController)
    {
        var memoryCacheOptions = new MemoryCacheOptions 
        {
            SizeLimit = 1000,
            ExpirationScanFrequency = TimeSpan.FromSeconds(10)
        };

        _cache = new MemoryCache(memoryCacheOptions);

        _queryable = dbController.GetQueryableCollection<Confectionary>();
        _collection = dbController.GetCollection<Confectionary>();
    }

    public Confectionary Get(string id)
    {
        return _cache.GetOrCreate(id, cacheEntry => {
            cacheEntry.Size = 1;
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

            var result = _queryable.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Failed to find item with id: {id}");
            }

            return result;
        });
    }

    public Confectionary Get(string id, bool bipassCache)
    {
        var result = _queryable.FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            throw new KeyNotFoundException($"Failed to find item with id: {id}");
        }

        _cache.Set(id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });

        return result;
    }


    public Confectionary Put(Confectionary confectionary)
    {
        _collection.InsertOne(confectionary);
        _cache.Set(confectionary.Id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });
        
        return confectionary;
    }

    public List<Confectionary> Get()
    {
        return _queryable.ToList();
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }
}