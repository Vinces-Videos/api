using Models;

namespace Services;

public interface IProductsService
{
    Product Get(string id);
    List<Product> Get();
    void Delete(string id);
    Product Put(Product product);
}