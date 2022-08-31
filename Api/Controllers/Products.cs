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
    private Repositories.IProducts _productRepo;

    //Dependency inject the IDatabaseController into the controller
    public ProductsController(IDatabaseController _db, Repositories.IProducts productRepo)
    {
        db = _db;
        _productRepo = productRepo;
    }

    [HttpGet(Name = "GetProducts")]
    public List<Product> Get() => db.GetCollectionRows<Product>();

    //Gets a database item by its Id and returns the result
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        if(!db.IsValidId(id))
            return BadRequest();

        try 
        {
            return Ok(_productRepo.Get(id));
        }
        catch(KeyNotFoundException)
        {
            return NotFound();
        }        
    }
}