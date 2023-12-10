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

    [HttpPost("api/areas/{areaId}/apartment")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GetApartmentsDto>> AddApartment(int areaId, CreateApartmentDto createApartmentDto)
    {
        try
        {
            var apartment = await apartmentsRepository.CreateApartment(areaId, createApartmentDto);

            return StatusCode(201, apartment);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

    /*
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
