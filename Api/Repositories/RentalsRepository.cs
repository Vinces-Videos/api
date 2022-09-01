using Database;
using Microsoft.Extensions.Caching.Memory;
using Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Repositories;

public class RentalsRepository : IRentalsRepository
{
    private IMemoryCache _cache;
    private IQueryable<Rental> _queryableRentals;
    private IDatabaseCollection<Rental> _rentalCollection;

    public RentalsRepository(IDatabaseContext dbController)
    {
        var memoryCacheOptions = new MemoryCacheOptions
        {
            SizeLimit = 1000,
            ExpirationScanFrequency = TimeSpan.FromSeconds(10)
        };

        _cache = new MemoryCache(memoryCacheOptions);

        _queryableRentals = dbController.GetQueryableCollection<Rental>();
        _rentalCollection = dbController.GetCollection<Rental>();
    }

    public Rental Get(string id)
    {
        return _cache.GetOrCreate(id, cacheEntry => {
            cacheEntry.Size = 1;
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

            var result = _queryableRentals.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Failed to find Rental with id: {id}");
            }

            return result;
        });
    }

    public Rental Get(string id, bool bipassCache)
    {
        var result = _queryableRentals.FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            throw new KeyNotFoundException($"Failed to find Rental with id: {id}");
        }

        _cache.Set(id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });

        return result;
    }


    public Rental Put(Rental rental)
    {
        _rentalCollection.InsertOne(rental);
        _cache.Set(rental.Id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });
        
        return rental;
    }

    public List<Rental> GetRentals()
    {
        return _queryableRentals.ToList();
    }

    public void Delete(string id)
    {
        _rentalCollection.DeleteOne(id);
    }
}