using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VideosController : ControllerBase
{
    [HttpGet(Name = "GetVideos")]
    public object Get()
    {
        return System.IO.File.ReadAllText(@"../Test.Data.Files/videos.json");
    }
}