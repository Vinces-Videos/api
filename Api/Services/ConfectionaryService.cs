using Models;

namespace Services;

public class ConfectionaryService : IConfectionaryService
{
    private Repositories.IDatabaseItemRepository<Confectionary> _repo;

    public ConfectionaryService(Repositories.IDatabaseItemRepository<Confectionary> repo)
    {
        _repo = repo;
    }

    public void Delete(string id)
    {
        _repo.Delete(id);
    }

    public Confectionary Get(string id)
    {
        return _repo.Get(id);
    }

    public List<Confectionary> Get()
    {
        return _repo.Get();
    }

    public Confectionary Put(Confectionary confectionary)
    {
        return _repo.Put(confectionary);
    }
}