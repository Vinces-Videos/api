using Microsoft.Extensions.Caching.Memory;
using Models;
using Database;

namespace Repositories;

public class InvoicesRepository : IInvoicesRepository
{
    private IMemoryCache _cache;
    private IQueryable<Invoice> _queryable;
    private IDatabaseCollection<Invoice> _collection;

    public InvoicesRepository(IDatabaseContext dbController)
    {
        var memoryCacheOptions = new MemoryCacheOptions 
        {
            SizeLimit = 1000,
            ExpirationScanFrequency = TimeSpan.FromSeconds(10)
        };

        _cache = new MemoryCache(memoryCacheOptions);

        _queryable = dbController.GetQueryableCollection<Invoice>();
        _collection = dbController.GetCollection<Invoice>();
    }

    public Invoice Get(string id)
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

    public Invoice Get(string id, bool bipassCache)
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


    public Invoice Put(Invoice invoice)
    {
        _collection.InsertOne(invoice);
        _cache.Set(invoice.Id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });
        
        return invoice;
    }

    public List<Invoice> GetInvoices()
    {
        return _queryable.ToList();
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }
}