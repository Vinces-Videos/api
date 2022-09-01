using Models;

namespace Repositories;

public interface IProductsRepository
{
    Product Get(string id);
    List<Product> GetProducts();
    void Delete(string id);
    Product Put(Product product);
}