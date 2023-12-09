using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models;
using RealEstateAPI.Services;

namespace RealEstateAPI.Controllers;

[Route("api/[controller]")]
public class AreasController : ControllerBase
{
    private readonly AreasRepository _areasRepository;

    public AreasController(AreasRepository areasRepository)
    {
        _areasRepository = areasRepository;
    }

    // GET: api/Areas
    [HttpGet]
    public async Task<ActionResult<GetAllAreasDto>> GetAllAreas()
    {
        try
        {
            var areas = await _areasRepository.GetAllAreasAsync();
            return Ok(areas);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
