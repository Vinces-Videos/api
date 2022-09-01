using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

namespace Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private Services.IProducts _productService;
    private IDatabaseValidations _databaseValidations;

    //Dependency inject the IDatabaseController into the controller
    public ProductsController(Services.IProducts productRepo, IDatabaseValidations databaseValidations)
    {
        _productService = productRepo;
        _databaseValidations = databaseValidations;
    }

    [HttpGet(Name = "GetProducts")]
    public List<Product> Get() => _productService.GetProducts();

    //Gets a database item by its Id and returns the result
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        if(!_databaseValidations.IsValidId(id))
            return BadRequest();

        return Ok(_productService.Get(id));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        if(!_databaseValidations.IsValidId(id))
            return BadRequest();

        _productService.Delete(id);

        return Ok();
    }

    [HttpPut]
    public IActionResult Update(Product product)
    {
        if(!_databaseValidations.IsValidId(product.Id))
            return BadRequest();

        _productService.Put(product);

        return Ok();
    }
}