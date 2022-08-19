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
}