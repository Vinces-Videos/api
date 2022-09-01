using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ConfectionaryController : ControllerBase
{
    private IConfectionaryService _confectionaryService;
    private IDatabaseValidations _dbValidations;

    //Dependency inject the IDatabaseController into the controller
    public ConfectionaryController(IConfectionaryService confectionaryService, IDatabaseValidations dbValidations)
    {
        _confectionaryService = confectionaryService;
        _dbValidations = dbValidations;
    }

    [HttpGet(Name = "GetConfectionary")]
    public List<Confectionary> Get() => _confectionaryService.Get();

    //Gets a database item by its Id and returns the result
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        if(!_dbValidations.IsValidId(id))
            return BadRequest();
        
        return Ok(_confectionaryService.Get(id));   
    }

    [HttpPost(Name = "CreateConfectionary")]
    public IActionResult Post(Confectionary input) => Content($"A new record has been inserted with an Id of {_confectionaryService.Put(input)}");

    [HttpDelete(Name = "DeleteConfectionary")]
    public IActionResult Delete(string id)
    {
        if(!_dbValidations.IsValidId(id))
            return BadRequest();

        _confectionaryService.Delete(id);
        return Ok();
    }
}