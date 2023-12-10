using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models;
using RealEstateAPI.Services;

namespace RealEstateAPI.Controllers;

[Route("api/[controller]"), ApiController]
public class HousesController(HousesRepository housesRepository) : ControllerBase
{
    // GET: api/Houses
    [HttpGet]
    public async Task<ActionResult<GetHousesDto>> GetHouses(
        int? areaId=null, double? minPrice = null, double? maxPrice = null,
        int? minRooms = null, int? maxRooms = null
    )
    {
        try
        {
            var houses = await housesRepository.GetAllHouses(
                areaId, minPrice, maxPrice, minRooms, maxRooms
                );

            return Ok(houses);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("api/areas/{id}/house")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GetHousesDto>> AddHouse(int id, [FromBody]CreateHouseDto createHouseDto)
    {
        try
        {
            var house = await housesRepository.CreateHouse(id, createHouseDto);
            return StatusCode(201, house);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("api/house/{houseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteHouse(int houseId)
    {
        try
        {
            var house = await housesRepository.DeleteHouse(houseId);

            if (!house.Any()) return NotFound();

            return NoContent();
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

    }

    [HttpGet("api/house/{houseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetHousesDto>> GetHouseById(int houseId)
    {
        try
        {
            var house = await housesRepository.GetHouseById(houseId);

            if (!house.Any()) return NotFound();

            return Ok(house);
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPatch("api/house/{houseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetHousesDto>> UpdateHouse(int houseId, UpdateHouseDto updateHouseDto)
    {
        try
        {
            var house = await housesRepository.UpdateHouse(houseId, updateHouseDto);

            if (!house.Any()) return NotFound();

            return Ok(house);
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}
