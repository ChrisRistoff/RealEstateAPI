using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RealEstateAPI.Models;


namespace RealEstateAPI.Controllers;

[Route("api/[controller]"), ApiController]
public class HousesController(RealEstateContext context) : ControllerBase
{
    // GET: api/Houses
    [HttpGet]
    public async Task<ActionResult<GetHousesDto>> GetHouses(
        int? areaId=null, double? minPrice = null, double? maxPrice = null,
        int? minRooms = null, int? maxRooms = null
    )
    {
        var sqlQuery = new StringBuilder("SELECT * FROM houses WHERE 1=1");
        var parameters = new List<object>();

        if (areaId.HasValue)
        {
            sqlQuery.Append(" AND \"areaId\" = {0}");
            parameters.Add(areaId.Value);
        }

        if (minPrice.HasValue)
        {
            sqlQuery.Append(" AND price >= {1}");
            parameters.Add(minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            sqlQuery.Append(" AND price <= {2}");
            parameters.Add(maxPrice.Value);
        }

        if (minRooms.HasValue)
        {
            sqlQuery.Append(" AND rooms >= {3}");
            parameters.Add(minRooms.Value);
        }

        if (maxRooms.HasValue)
        {
            sqlQuery.Append(" AND rooms <= {4}");
            parameters.Add(maxRooms.Value);
        }

        var houses = await context.houses
            .FromSqlRaw(sqlQuery.ToString(), parameters.ToArray())
            .AsNoTracking()
            .ToListAsync();

        return Ok(houses);
    }

    [HttpPost("api/areas/{id}/house")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GetHousesDto>> AddHouse(int id, CreateHouseDto createHouseDto)
    {
        string query = "INSERT INTO houses (\"areaId\", description, price, address, postcode, " +
                    "\"sqrFeet\", rooms, bathrooms, \"parkingSpaces\", furnished) " +
                    "VALUES (@AreaId, @Description, @Price, @Address, @Postcode, " +
                    "@SqrFeet, @Rooms, @Bathrooms, @ParkingSpaces, @Furnished) " +
                    "RETURNING *;";

        var house = await context.houses.FromSqlRaw(query,
            new NpgsqlParameter("@AreaId", id),
            new NpgsqlParameter("@Description", createHouseDto.description),
            new NpgsqlParameter("@Price", createHouseDto.price),
            new NpgsqlParameter("@Address", createHouseDto.address),
            new NpgsqlParameter("@Postcode", createHouseDto.postcode),
            new NpgsqlParameter("@SqrFeet", createHouseDto.sqrFeet),
            new NpgsqlParameter("@Rooms", createHouseDto.rooms),
            new NpgsqlParameter("@Bathrooms", createHouseDto.bathrooms),
            new NpgsqlParameter("@ParkingSpaces", createHouseDto.parkingSpaces),
            new NpgsqlParameter("@Furnished", createHouseDto.furnished)
        ).ToListAsync();

        if(house != null) return StatusCode(StatusCodes.Status201Created, house);

        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("api/house/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteHouse(int id)
    {
        try
        {
            var house = await context.houses.FindAsync(id);

            if (house == null) return NotFound();

            context.houses.Remove(house);
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("api/house/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetHousesDto>> GetHouseById(int id)
    {
        try
        {
            var house = await context.houses.FindAsync(id);
            if (house == null) return NotFound();

            return Ok(house);
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
