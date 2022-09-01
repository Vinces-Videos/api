using Models;

namespace Services;

public interface IFilmCategoryService 
{
    List<FilmCategory> Get();
    FilmCategory Get(string id);
    void Delete(string id);
    FilmCategory Put(FilmCategory filmCategory);
}