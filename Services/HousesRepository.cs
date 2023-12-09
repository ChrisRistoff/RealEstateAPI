using System.Text;
using Dapper;
using Npgsql;
using RealEstateAPI.Models;

namespace RealEstateAPI.Services;

public abstract class HousesRepository(IConfiguration configuration)
{
    public async Task<IEnumerable<GetHousesDto>> GetAllHouses(
        int? areaId = null, double? minPrice = null, double? maxPrice = null,
        int? minRooms = null, int? maxRooms = null
    )
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        var query = new StringBuilder("SELECT * FROM HOUSES WHERE 1=1");
        var parameters = new DynamicParameters();

        if (areaId.HasValue)
        {
            query.Append(" AND \"areaId\" = @\"areaId\"")
            parameters.Add("\"areaId\"", areaId.Value);
        }

        if (minPrice.HasValue)
        {
            query.Append(" AND price >= @\"minPrice\"");
            parameters.Add("\"minPrice\"", minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query.Append(" AND price <= @\"maxPrice\"");
            parameters.Add("\"maxPrice\"", maxPrice.Value);
        }

        if (minRooms.HasValue)
        {
            query.Append(" AND rooms >= @\"minRooms\"");
            parameters.Add("\"minRooms\"", minRooms.Value);
        }

        if (maxRooms.HasValue)
        {
            query.Append(" AND rooms <= @\"maxRooms\"");
            parameters.Add("\"maxRooms\"", maxRooms.Value);
        }

        return await connection.QueryAsync<GetHousesDto>(query.ToString());
    }

    public async Task<IEnumerable<CreateHouseDto>> CreateHouse(CreateHouseDto createHouseDto)
    {

    }
}
