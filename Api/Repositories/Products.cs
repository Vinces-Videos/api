

using Microsoft.Extensions.Caching.Memory;
using Models;
using Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Repositories;

public class Products : IProducts
{
    private IMemoryCache _cache;
    private IMongoQueryable<Product> _queryableProducts;
    private IMongoCollection<Product> _productCollection;

    public Products(IDatabaseContext dbController)
    {
        var memoryCacheOptions = new MemoryCacheOptions 
        {
            SizeLimit = 1000,
            ExpirationScanFrequency = TimeSpan.FromSeconds(10)
        };

        _cache = new MemoryCache(memoryCacheOptions);

        _queryableProducts = dbController.GetQueryableCollection<Product>();
        _productCollection = dbController.GetCollection<Product>();
    }

    public Product Get(string id)
    {
        return _cache.GetOrCreate(id, cacheEntry => {
            cacheEntry.Size = 1;
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

            var result = _queryableProducts.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Failed to find Product with id: {id}");
            }

            return result;
        });
    }

    public Product Get(string id, bool bipassCache)
    {
        var result = _queryableProducts.FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            throw new KeyNotFoundException($"Failed to find Product with id: {id}");
        }

        _cache.Set(id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });

        return result;
    }


    public void Update(Product product)
    {
        _productCollection.InsertOne(product);
        _cache.Set(product.Id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });
    }

    public List<Product> GetProducts()
    {
        return _queryableProducts.ToList();
    }
}