using Models;

namespace Component.Products.Services;

public class Products : IProducts
{
    private Repositories.IProducts _productRepo;

    public Products(Repositories.IProducts productRepo)
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

    public List<Product> GetProducts()
    {
        return _productRepo.GetProducts();
    }

    public Product Put(Product product)
    {
        return _productRepo.Put(product);
    }
}