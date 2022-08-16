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
        return JsonSerializer.Serialize<List<Product>>(MongoController.GetCollection<Product>("Products"));
    }
}