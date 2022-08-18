using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

namespace Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private IDatabaseController db;

    //Dependency inject the IDatabaseController into the controller
    public ProductsController(IDatabaseController _db)
    {
        db = _db;
    }

    [HttpGet(Name = "GetProducts")]
    public List<Product> Get()
    {
        //Content formats the JSON result correctly.
        return db.GetCollection<Product>();
    }

    //Gets a product by it's Id from the database and returns it, or a bad request if the id was not valid
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        if(!db.IsValidId(id))
            return BadRequest();

        try 
        {
            return Ok(db.GetById<Product>(id));
        }
        catch(KeyNotFoundException)
        {
            return NotFound();
        }
        
    }
}