using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
using Services;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CustomersController : ControllerBase
{
    private IDatabaseValidations _databaseValidations;

    private ICustomersService _customersService;

    //Dependency inject the IDatabaseController into the controller
    public CustomersController(ICustomersService customersService, IDatabaseValidations databaseValidations)
    {
        _customersService = customersService;
        _databaseValidations = databaseValidations;
    }

    [HttpGet(Name = "GetCustomers")]
    public List<Customer> Get() => _customersService.Get();

    [HttpPost(Name = "CreateCustomer")]
    public IActionResult Post(Customer input)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        if(!_databaseValidations.IsValidId(id))
            return BadRequest();

        _customersService.Delete(id);

        return Ok();
    }

    [HttpPut]
    public IActionResult Update(Customer customer)
    {
        if(!_databaseValidations.IsValidId(customer.Id))
            return BadRequest();

        _customersService.Put(customer);

        return Ok();
    }
}