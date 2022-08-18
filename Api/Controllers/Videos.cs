using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using Utilities;
using Models;

namespace Controllers;

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
    public IActionResult Get()
    {
        //Content formats the JSON result correctly.
        return Content(JsonSerializer.Serialize<List<Product>>(db.GetCollection<Product>("Products")));
    }

    //[HttpGet("{id}")]
    //public IActionResult Get(string id) => Content(JsonSerializer.Serialize<Product>(db.GetById<Product>(id, "Products")) ?? $"Video with object Id {id} was not found");

    [HttpGet("{id}")]
    public Product Get(string id) => db.GetById<Product>(id, "Products");

}