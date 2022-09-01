using Microsoft.AspNetCore.Mvc;
using Database;
using Services;
using Models;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class RentalController : ControllerBase
{
    private IDatabaseValidations _dbValidations;
    private IRentalsService _rentalService;

    public RentalController(IDatabaseValidations dbValidations, IRentalsService rentalService)
    {
        _dbValidations = dbValidations;
    }
}