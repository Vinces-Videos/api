using Models;

namespace Services;

public class RentalsService : IRentalsService
{
    private Repositories.IRentalsRepository _rentalsRepo;

    public RentalsService(Repositories.IRentalsRepository rentalsRepo)
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

    public List<Rental> GetRentals()
    {
        return _rentalsRepo.GetRentals();
    }

    public Rental Put(Rental Rental)
    {
        return _rentalsRepo.Put(Rental);
    }
}