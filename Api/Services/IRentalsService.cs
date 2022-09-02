using Models;

namespace Services;

public interface IRentalsService
{
    Rental Get(string id);
    List<Rental> Get();
    void Delete(string id);
    Rental Put(Rental rental);
}