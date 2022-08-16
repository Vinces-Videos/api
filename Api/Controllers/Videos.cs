using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Utilities;
using Models;

namespace Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class VideosController : ControllerBase
{
    [HttpGet(Name = "GetVideos")]
    public object Get()
    {
        return string.Join(", ", MongoController.GetCollection<Product>("Products").Select(product => product.Title));
        //return JsonSerializer.Deserialize<dynamic>(System.IO.File.ReadAllText(@"../Test.Data.Files/videos.json")) ?? new dynamic[0];
    }
}