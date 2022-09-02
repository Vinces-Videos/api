using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class RentalsController : ControllerBase
{
    private IRentalsService _rentalService;
    private IDatabaseValidations _databaseValidations;

    //Dependency inject the IDatabaseController into the controller
    public RentalsController(IRentalsService rentalRepo, IDatabaseValidations databaseValidations)
    {
        _rentalService = rentalRepo;
        _databaseValidations = databaseValidations;
    }

    [HttpGet(Name = "GetRentals")]
    public List<Rental> Get() => _rentalService.Get();

    //Gets a database item by its Id and returns the result
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        if(!_databaseValidations.IsValidId(id))
            return BadRequest();

        return Ok(_rentalService.Get(id));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        if(!_databaseValidations.IsValidId(id))
            return BadRequest();

        _rentalService.Delete(id);

        return Ok();
    }

    [HttpPut]
    public IActionResult Update(Rental Rental)
    {
        if(!_databaseValidations.IsValidId(Rental.Id))
            return BadRequest();

        _rentalService.Put(Rental);

        return Ok();
    }
}