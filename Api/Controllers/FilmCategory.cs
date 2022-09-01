using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class FilmCategoryController : ControllerBase
{
    private IFilmCategoryService _filmCategoryService;

    //Dependency inject the IDatabaseController into the controller
    public FilmCategoryController(IFilmCategoryService filmCategoryService)
    {
        _filmCategoryService = filmCategoryService;
    }

    [HttpGet(Name = "GetFilmCategories")]
    public List<FilmCategory> Get() => _filmCategoryService.Get();

    [HttpPost(Name ="CreateFilmCategory")]
    public IActionResult Post(FilmCategory input) => Content($"A new record has been inserted with an Id of {_filmCategoryService.Put(input)}");
}