using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

namespace Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ConfectionaryController : ControllerBase
{
    private IDatabaseController db;

    //Dependency inject the IDatabaseController into the controller
    public ConfectionaryController(IDatabaseController _db)
    {
        db = _db;
    }

    [HttpGet(Name = "GetConfectionary")]
    public List<Confectionary> Get() => db.GetCollectionByType<Confectionary>();

    //Gets a database item by its Id and returns the result
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        if(!db.IsValidId(id))
            return BadRequest();

        try 
        {
            return Ok(db.GetById<Confectionary>(id));
        }
        catch(KeyNotFoundException)
        {
            return NotFound();
        }        
    }

    [HttpPost(Name = "CreateConfectionary")]
    public IActionResult Post(Confectionary input) => Content($"A new record has been inserted with an Id of {db.Insert<Confectionary>(input)}");

    [HttpDelete(Name = "DeleteConfectionary")]
    public IActionResult Delete(string id)
    {
        if(!db.IsValidId(id))
            return BadRequest();

        try 
        {
            return Ok(db.DeleteById<Confectionary>(id));
        }
        catch(KeyNotFoundException)
        {
            return NotFound();
        }   
    }
}