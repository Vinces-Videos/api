using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class VideosController : ControllerBase
{
    [HttpGet(Name = "GetVideos")]
    public object Get()
    {
        return JsonSerializer.Deserialize<dynamic>(System.IO.File.ReadAllText(@"../Test.Data.Files/videos.json")) ?? new dynamic[0];
    }
}