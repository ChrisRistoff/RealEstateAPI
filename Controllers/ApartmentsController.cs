using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers;

[Route("api/[controller]"), ApiController]
public class ApartmentsController : ControllerBase
{
    private readonly RealEstateContext _context;

    public ApartmentsController(RealEstateContext context)
    {
        _context = context;
    }

        // GET: api/apartments
        [HttpGet]
        public async Task<ActionResult<Apartment>> GetApartments(
            int? areaId=null, double? minPrice = null, double? maxPrice = null,
            int? minRooms = null, int? maxRooms = null
        )
        {
            var sqlQuery = new StringBuilder("SELECT * FROM apartments WHERE 1=1");
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

            var apartments = await _context.houses
                .FromSqlRaw(sqlQuery.ToString(), parameters.ToArray())
                .AsNoTracking()
                .ToListAsync();

            return Ok(apartments);
        }

        [HttpPost("api/areas/{id}/apartment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Apartment>> AddApartment(int id, CreateApartmentDto createApartmentDto)
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

            if (apartment != null) return Ok(apartment);

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("api/apartments/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Apartment>> DeleteApartment(int id)
        {
            try
            {
                var apartment = await _context.apartments.FindAsync(id);

                if (apartment == null) return NotFound();

                _context.apartments.Remove(apartmen);
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
        public async Task<ActionResult<Apartment>> GetApartmentById(int id)
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
        }
}
