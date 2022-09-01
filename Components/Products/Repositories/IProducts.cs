using Models;

namespace Component.Products.Repositories;

internal interface IProducts
{
    Product Get(string id);
    List<Product> GetProducts();
    void Delete(string id);
    Product Put(Product product);
}