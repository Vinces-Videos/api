using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    [HttpGet(Name = "GetDemo")]
    public string Get() => "I am returning some demo data!";

    //This demonstrates using path parameters i.e. localhost:<port>/videos/4
    [HttpGet("{id}")]
    public List<string> Get(int Id) => new List<string> { "Demo Data", "Some Other Demo Data" };
}