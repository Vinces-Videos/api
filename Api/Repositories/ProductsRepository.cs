

using Microsoft.Extensions.Caching.Memory;
using Models;
using Database;

namespace Repositories;

public class ProductsRepository : IProductsRepository
{
    private IMemoryCache _cache;
    private IQueryable<Product> _queryableProducts;
    private IDatabaseCollection<Product> _productCollection;

    public ProductsRepository(IDatabaseContext dbController)
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


    public Product Put(Product product)
    {
        _productCollection.InsertOne(product);
        _cache.Set(product.Id, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });
        
        return product;
    }

    public List<Product> GetProducts()
    {
        return _queryableProducts.ToList();
    }

    public void Delete(string id)
    {
        _productCollection.DeleteOne(id);
    }
}