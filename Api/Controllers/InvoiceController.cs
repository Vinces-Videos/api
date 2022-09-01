using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class InvoiceController : ControllerBase
{
    private IDatabaseContext db;

    //Dependency inject the IDatabaseController into the controller
    public InvoiceController(IDatabaseContext _db)
    {
        db = _db;
    }

    [HttpGet(Name = "GetInvoices")]
    public List<Invoice> Get() => db.GetCollectionByType<Invoice>();
}