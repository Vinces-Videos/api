using Models;

namespace Services;

public class ConfectionaryService : IConfectionaryService
{
    private Repositories.IConfectionaryRepository _repo;

    public ConfectionaryService(Repositories.IConfectionaryRepository repo)
    {
        _repo = repo;
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Confectionary Get(string id)
    {
        throw new NotImplementedException();
    }

    public List<Confectionary> Get()
    {
        throw new NotImplementedException();
    }

    public Confectionary Put(Confectionary confectionary)
    {
        throw new NotImplementedException();
    }
}