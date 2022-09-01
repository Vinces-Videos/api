using Models;

namespace Component.Products.Services;

public interface IProducts
{
    Product Get(string id);
    List<Product> GetProducts();
    void Delete(string id);
    Product Put(Product product);
}