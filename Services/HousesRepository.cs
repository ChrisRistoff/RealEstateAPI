using System.Text;
using Dapper;
using Npgsql;
using RealEstateAPI.Models;

namespace RealEstateAPI.Services;

public class HousesRepository(IConfiguration configuration)
{

    public async Task<IEnumerable<GetHousesDto>> GetAllHouses(
        int? areaId = null, double? minPrice = null, double? maxPrice = null,
        int? minRooms = null, int? maxRooms = null
    )
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        var query = new StringBuilder("SELECT * FROM houses WHERE 1=1");
        var parameters = new DynamicParameters();

        if (areaId.HasValue)
        {
            query.Append(" AND \"areaId\" = @areaId");
            parameters.Add("areaId", areaId.Value);
        }

        if (minPrice.HasValue)
        {
            query.Append(" AND price >= @minPrice");
            parameters.Add("minPrice", minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query.Append(" AND price <= @maxPrice");
            parameters.Add("maxPrice", maxPrice.Value);
        }

        if (minRooms.HasValue)
        {
            query.Append(" AND rooms >= @minRooms");
            parameters.Add("minRooms", minRooms.Value);
        }

        if (maxRooms.HasValue)
        {
            query.Append(" AND rooms <= @maxRooms");
            parameters.Add("maxRooms", maxRooms.Value);
        }

        return await connection.QueryAsync<GetHousesDto>(query.ToString(), parameters);
    }

    public async Task<IEnumerable<GetHousesDto>> CreateHouse(int id, CreateHouseDto createHouseDto)
    {
        var parameters = new DynamicParameters(createHouseDto);
        parameters.Add("areaId", id);

        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        string query = "INSERT INTO houses (\"areaId\", description, price, address, postcode, " +
                       "\"sqrFeet\", rooms, bathrooms, \"parkingSpaces\", furnished) " +
                       "VALUES (@AreaId, @Description, @Price, @Address, @Postcode, " +
                       "@SqrFeet, @Rooms, @Bathrooms, @ParkingSpaces, @Furnished) " +
                       "RETURNING *;";

        return await connection.QueryAsync<GetHousesDto>(query, parameters);
    }

    public async Task<IEnumerable<dynamic>> DeleteHouse(int houseId)
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        string query = "DELETE FROM houses WHERE \"houseId\" = @houseId RETURNING *";

        return await connection.QueryAsync(query, new { HouseId = houseId});
    }

    public async Task<IEnumerable<GetHousesDto>> GetHouseById (int houseId)
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        string query = "SELECT * FROM houses WHERE \"houseId\" = @houseId";

        return await connection.QueryAsync<GetHousesDto>(query, new { HouseId = houseId });
    }
}
