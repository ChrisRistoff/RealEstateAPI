using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RealEstateAPI.Models;
using RealEstateAPI.Services;

namespace RealEstateAPI.Controllers;

[Route("api/[controller]"), ApiController]
public class ApartmentsController(ApartmentsRepository apartmentsRepository) : ControllerBase
{

    // GET: api/apartments
    [HttpGet]
    public async Task<ActionResult<GetApartmentsDto>> GetApartments(
        int? areaId = null, double? minPrice = null, double? maxPrice = null,
        int? minRooms = null, int? maxRooms = null
    )
    {
        try
        {
            var apartments = await apartmentsRepository.GetAllApartments(
                areaId, minPrice, maxPrice, minRooms, maxRooms
                );

            return Ok(apartments);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /*
    [HttpPost("api/areas/{id}/apartment")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GetApartmentsDto>> AddApartment(int id, CreateApartmentDto createApartmentDto)
    {
        string query = "INSERT INTO apartments (\"areaId\", description, price, address, postcode, " +
                             "\"sqrFeet\", rooms, bathrooms, \"parkingSpaces\", furnished) " +
                             "VALUES (@AreaId, @Description, @Price, @Address, @Postcode, " +
                             "@SqrFeet, @Rooms, @Bathrooms, @ParkingSpaces, @Furnished) " +
                             "RETURNING *;";

        var apartment = await _context.apartments.FromSqlRaw(query,
            new NpgsqlParameter("@AreaId", id),
            new NpgsqlParameter("@Description", createApartmentDto.description),
            new NpgsqlParameter("@Price", createApartmentDto.price),
            new NpgsqlParameter("@Address", createApartmentDto.address),
            new NpgsqlParameter("@Postcode", createApartmentDto.postcode),
            new NpgsqlParameter("@SqrFeet", createApartmentDto.sqrFeet),
            new NpgsqlParameter("@Rooms", createApartmentDto.rooms),
            new NpgsqlParameter("@Bathrooms", createApartmentDto.bathrooms),
            new NpgsqlParameter("@ParkingSpaces", createApartmentDto.parkingSpaces),
            new NpgsqlParameter("@Furnished", createApartmentDto.furnished)
            ).ToListAsync();

        if(apartment != null) return StatusCode(StatusCodes.Status201Created, apartment);

        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("api/apartments/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteApartment(int id)
    {
        try
        {
            var apartment = await _context.apartments.FindAsync(id);

            if (apartment == null) return NotFound();

            _context.apartments.Remove(apartment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("api/apartments/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetApartmentsDto>> GetApartmentById(int id)
    {
        try
        {
            var apartment = await _context.apartments.FindAsync(id);
            if (apartment == null) return NotFound();

            return Ok(apartment);
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }*/
}
