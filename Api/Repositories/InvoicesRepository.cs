using Microsoft.Extensions.Caching.Memory;
using Models;
using Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Repositories;

public class InvoicesRepository : IInvoicesRepository
{
    private IMemoryCache _cache;
    private IQueryable<Invoice> _queryableInvoices;
    private IDatabaseCollection<Invoice> _invoiceCollection;

    public InvoicesRepository(IDatabaseContext dbController)
    {
        var memoryCacheOptions = new MemoryCacheOptions 
        {
            SizeLimit = 1000,
            ExpirationScanFrequency = TimeSpan.FromSeconds(10)
        };

        _cache = new MemoryCache(memoryCacheOptions);

        _queryableInvoices = dbController.GetQueryableCollection<Invoice>();
        _invoiceCollection = dbController.GetCollection<Invoice>();
    }

    public Invoice Get(string id)
    {
        return _cache.GetOrCreate(id, cacheEntry => {
            cacheEntry.Size = 1;
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

            var result = _queryableInvoices.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Failed to find item with id: {id}");
            }

            return result;
        });
    }

    public Invoice Get(string id, bool bipassCache)
    {
        var result = _queryableInvoices.FirstOrDefault(x => x.Id == id);

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
        _invoiceCollection.InsertOne(invoice);
        _cache.Set(invoice.Id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });
        
        return invoice;
    }

    public List<Invoice> GetInvoices()
    {
        return _queryableInvoices.ToList();
    }

    public void Delete(string id)
    {
        _invoiceCollection.DeleteOne(id);
    }
}