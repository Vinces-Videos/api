using Models;

namespace Services;

public interface IRentalsService
{
    Rental Get(string id);
    List<Rental> GetRentals();
    void Delete(string id);
    Rental Put(Rental rental);
}