using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class FilmCategoryController : ControllerBase
{
    private IDatabaseContext db;

    //Dependency inject the IDatabaseController into the controller
    public FilmCategoryController(IDatabaseContext _db)
    {
        db = _db;
    }

    [HttpGet(Name = "GetFilmCategories")]
    public List<FilmCategory> Get() => db.GetCollectionByType<FilmCategory>();

    [HttpPost(Name ="CreateFilmCategory")]
    public IActionResult Post(FilmCategory input) => Content($"A new record has been inserted with an Id of {db.Insert<FilmCategory>(input)}");
}