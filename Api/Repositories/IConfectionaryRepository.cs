using Models;

namespace Repositories;

//An interface for an Invoice repository object
public interface IConfectionaryRepository
{
    List<Confectionary> Get();
    Confectionary Get(string id);
    void Delete(string id);
    Confectionary Put(Confectionary confectionary);
}