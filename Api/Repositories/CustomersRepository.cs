using Microsoft.Extensions.Caching.Memory;
using Models;
using Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Repositories;

public class CustomersRepository : ICustomersRepository
{
    private IMemoryCache _cache;
    private IQueryable<Customer> _queryable;
    private IDatabaseCollection<Customer> _collection;

    public CustomersRepository(IDatabaseContext dbController)
    {
        var memoryCacheOptions = new MemoryCacheOptions 
        {
            SizeLimit = 1000,
            ExpirationScanFrequency = TimeSpan.FromSeconds(10)
        };

        _cache = new MemoryCache(memoryCacheOptions);

        _queryable = dbController.GetQueryableCollection<Customer>();
        _collection = dbController.GetCollection<Customer>();
    }

    public Customer Get(string id)
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

    public Customer Get(string id, bool bipassCache)
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


    public Customer Put(Customer customer)
    {
        _collection.InsertOne(customer);
        _cache.Set(customer.Id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });
        
        return customer;
    }

    public List<Customer> Get()
    {
        return _queryable.ToList();
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }
}