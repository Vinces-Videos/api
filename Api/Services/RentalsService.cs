using Models;

namespace Services;

public class RentalsService : IRentalsService
{
    private Repositories.IDatabaseItemRepository<Rental> _rentalsRepo;

    public RentalsService(Repositories.IDatabaseItemRepository<Rental> rentalsRepo)
    {
        _rentalsRepo = rentalsRepo;
    }

    public void Delete(string id)
    {
        _rentalsRepo.Delete(id);
    }

    public Rental Get(string id)
    {
        return _rentalsRepo.Get(id);
    }

    public List<Rental> Get()
    {
        return _rentalsRepo.Get();
    }

    public Rental Put(Rental Rental)
    {
        return _rentalsRepo.Put(Rental);
    }
}