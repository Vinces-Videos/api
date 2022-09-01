using Models;

namespace Repositories;

//An interface for an Invoice repository object
public interface ICustomersRepository
{
    List<Customer> Get();
    Customer Get(string id);
    void Delete(string id);
    Customer Put(Customer customer);
}