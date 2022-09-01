using Models;

namespace Services;

public class CustomersService : ICustomersService
{
    private Repositories.ICustomersRepository _repo;

    public CustomersService(Repositories.ICustomersRepository repo)
    {
        _repo = repo;
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Customer Get(string id)
    {
        throw new NotImplementedException();
    }

    public List<Customer> Get()
    {
        throw new NotImplementedException();
    }

    public Customer Put(Customer customer)
    {
        throw new NotImplementedException();
    }
}