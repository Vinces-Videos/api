using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private IDatabaseController db;

    public DemoController(IDatabaseController _db)
    {
        db = _db;
    }

    [HttpGet(Name = "GetDemo")]
    public string Get() => db.GetType().ToString();

    //This demonstrates using path parameters i.e. localhost:<port>/videos/4
    [HttpGet("{id}")]
    public List<string> Get(int Id) => new List<string> { "Demo Data", "Some Other Demo Data" };
}