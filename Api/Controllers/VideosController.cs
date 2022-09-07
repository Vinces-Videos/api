using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
using Services;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class VideosController : ControllerBase
{
    private IVideosService _videosService;
    private IFilmCategoryService _categoryService;
    private IDatabaseValidations _databaseValidations;
    
    //To be removed, this controller should have no direct DB access
    private IDatabaseContext _db;

    //Dependency inject the IDatabaseController into the controller
    public VideosController(IDatabaseContext db, IVideosService videosService, IFilmCategoryService categoryService, IDatabaseValidations databaseValidations)
    {
        _videosService = videosService;
        _categoryService = categoryService;
        _databaseValidations = databaseValidations;
        _db = db;
    }

    //Returns a list of videos
    [HttpGet(Name = "GetVideos")]
    public List<Video> Get() => _videosService.Get();

    //Inserts a new video, but before insertion is completed we perform a lookup on the Film Category ID as all Film Categories must have one.
    [HttpPut(Name = "CreateVideo")]
    public IActionResult Put(Video input)
    {
        if (_db.GetByName<FilmCategory>(input.Category) != null)
        {
            return Content($"A new record has been inserted with an Id of {_videosService.Put(input).Id}");
        }
        else
        {
            return Content($"Unable to locate category with an id of {input.Category}, all videos must have a valid category");
        }
    }
}