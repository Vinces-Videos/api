using Models;

namespace Services;

public class CustomersService : ICustomersService
{
    private Repositories.IDatabaseItemRepository<Customer> _repo;

    public CustomersService(Repositories.IDatabaseItemRepository<Customer> repo)
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