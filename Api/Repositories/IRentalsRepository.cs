using Models;

namespace Repositories;

public interface IRentalsRepository
{
    Rental Get(string id);
    List<Rental> GetRentals();
    void Delete(string id);
    Rental Put(Rental product);
}