using Models;

namespace Repositories;

public interface IProducts
{
    Product Get(string id);

    List<Product> GetProducts();
}