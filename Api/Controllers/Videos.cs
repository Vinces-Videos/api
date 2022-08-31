using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class VideosController : ControllerBase
{
    private IDatabaseController db;

    //Dependency inject the IDatabaseController into the controller
    public VideosController(IDatabaseController _db)
    {
        db = _db;
    }

    [HttpGet(Name = "GetVideos")]
    public List<Video> Get() => db.GetCollectionByType<Video>();

    [HttpPost(Name = "CreateVideo")]
    public IActionResult Post(Video input)
    {
        if (db.GetByName<FilmCategory>(input.Category).Count > 0)
        {
            return Content($"A new record has been inserted with an Id of {db.Insert<Video>(input)}");
        }
        else
        {
            return Content($"Unable to locate category with name {input.Category}, all videos must have a valid category");
        }
    }
}