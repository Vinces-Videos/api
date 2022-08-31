using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

namespace Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private Repositories.IProducts _productRepo;
    private IDatabaseValidations _databaseValidations;

    //Dependency inject the IDatabaseController into the controller
    public ProductsController(Repositories.IProducts productRepo, IDatabaseValidations databaseValidations)
    {
        _productRepo = productRepo;
        _databaseValidations = databaseValidations;
    }

    [HttpGet(Name = "GetProducts")]
    public List<Product> Get() => _productRepo.GetProducts();

    //Gets a database item by its Id and returns the result
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        if(!_databaseValidations.IsValidId(id))
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