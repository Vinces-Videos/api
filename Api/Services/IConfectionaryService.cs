using Models;

namespace Services;

public interface IConfectionaryService 
{
    List<Confectionary> Get();
    Confectionary Get(string id);
    void Delete(string id);
    Confectionary Put(Confectionary confectionary);
}