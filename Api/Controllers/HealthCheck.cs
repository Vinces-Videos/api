using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class HealthCheckController : ControllerBase
{
    [HttpGet(Name = "GetHealth")]
    public string Get() => "API is alive."; 
}