using Models;

namespace Services;

public interface ICustomersService 
{
    List<Customer> Get();
    Customer Get(string id);
    void Delete(string id);
    Customer Put(Customer customer);
}