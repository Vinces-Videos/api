using Models;
using Repositories;

namespace Services;

public class ProductsService : IProductsService
{
    private IDatabaseItemRepository<Product> _productRepo;

    public ProductsService(IDatabaseItemRepository<Product> productRepo)
    {
        _productRepo = productRepo;
    }

    public void Delete(string id)
    {
        _productRepo.Delete(id);
    }

    public Product Get(string id)
    {
        return _productRepo.Get(id);
    }

    public List<Product> Get()
    {
        return _productRepo.Get();
    }

    public Product Put(Product product)
    {
        return _productRepo.Put(product);
    }
}