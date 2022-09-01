using Models;
using Repositories;

namespace Services;

public class FilmCategoryService : IFilmCategoryService
{
    private IDatabaseItemRepository<FilmCategory> _filmCategoryRepo;

    public FilmCategoryService(IDatabaseItemRepository<FilmCategory> filmCategoryRepo)
    {
        _filmCategoryRepo = filmCategoryRepo;
    }

    public void Delete(string id)
    {
        _filmCategoryRepo.Delete(id);
    }

    public List<FilmCategory> Get()
    {
        return _filmCategoryRepo.Get();
    }

    public FilmCategory Get(string id)
    {
        return _filmCategoryRepo.Get(id);
    }

    public FilmCategory Put(FilmCategory filmCategory)
    {
        return _filmCategoryRepo.Put(filmCategory);
    }
}