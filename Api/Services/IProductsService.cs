using Models;

namespace Services;

public interface IProductsService
{
    Product Get(string id);
    List<Product> GetProducts();
    void Delete(string id);
    Product Put(Product product);
}