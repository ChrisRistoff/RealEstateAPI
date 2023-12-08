using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers;

[Route("api/[controller]")]
public class AreasController(RealEstateContext context) : ControllerBase
{

    // GET: api/Areas
    [HttpGet]
    public async Task<ActionResult<GetAllAreasDto>> GetAllAreas()
    {
        var query = "SELECT a.\"areaId\", a.name, a.description, a.schools, a.shops, a.kindergardens, " +
                    "COUNT(DISTINCT h.\"houseId\") as houseC, " +
                    "COUNT(DISTINCT ap.\"apartmentId\") as apartmentC " +
                    "FROM areas a " +
                    "LEFT JOIN houses h ON h.\"areaId\" = a.\"areaId\" " +
                    "LEFT JOIN apartments ap ON ap.\"areaId\" = a.\"areaId\" " +
                    "GROUP BY a.\"areaId\", a.name, a.description, a.schools, a.shops, a.kindergardens;";

        var areaEntities = await context.areas.FromSqlRaw(query).AsNoTracking().ToListAsync();

        var areas = areaEntities.Select(a => new GetAllAreasDto
        {
            areaId = a.areaId,
            name = a.name,
            description = a.description,
            schools = a.schools,
            shops = a.shops,
            kindergardens = a.kindergardens,
            houseC = a.houseC,
            apartmentC = a.apartmentC
        }).ToList();

        return Ok(areas);

    }
}
