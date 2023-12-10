using System.Text;
using Dapper;
using Npgsql;
using RealEstateAPI.Models;

namespace RealEstateAPI.Services;

public class ApartmentsRepository(IConfiguration configuration)
{

    public async Task<IEnumerable<GetApartmentsDto>> GetAllApartments(
        int? areaId = null, double? minPrice = null, double? maxPrice = null,
        int? minRooms = null, int? maxRooms = null
    )
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        var query = new StringBuilder("SELECT * FROM apartments WHERE 1=1");
        var parameters = new DynamicParameters();

        if (areaId.HasValue)
        {
            query.Append($" AND \"areaId\" = @AreaId");
            parameters.Add("AreaId", areaId.Value);
            Console.WriteLine(areaId.Value);
        }

        if (minPrice.HasValue)
        {
            query.Append(" AND price >= @MinPrice");
            parameters.Add("MinPrice", minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query.Append(" AND price <= @MaxPrice");
            parameters.Add("MaxPrice", maxPrice.Value);
        }

        if (minRooms.HasValue)
        {
            query.Append(" AND rooms >= @MinRooms");
            parameters.Add("MinRooms", minRooms.Value);
        }

        if (maxRooms.HasValue)
        {
            query.Append(" AND rooms <= @MaxRooms");
            parameters.Add("MaxRooms", maxRooms.Value);
        }

        return await connection.QueryAsync<GetApartmentsDto>(query.ToString(), parameters);
    }

    public async Task<IEnumerable<GetApartmentsDto>> CreateApartment(int areaId, CreateApartmentDto createApartmentDto)
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        var parameters = new DynamicParameters(createApartmentDto);
        parameters.Add("areaId", areaId);

        string query = "INSERT INTO apartments (\"areaId\", description, price, address, postcode, " +
                       "\"sqrFeet\", rooms, bathrooms, \"parkingSpaces\", furnished) " +
                       "VALUES (@AreaId, @Description, @Price, @Address, @Postcode, " +
                       "@SqrFeet, @Rooms, @Bathrooms, @ParkingSpaces, @Furnished) " +
                       "RETURNING *;";

        return await connection.QueryAsync<GetApartmentsDto>(
            query, parameters);

    }

    public async Task<IEnumerable<GetApartmentsDto>> DeleteApartment(int apartmentId)
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        string query = "DELETE FROM apartments WHERE \"apartmentId\" = @apartmentId RETURNING *";

        return await connection.QueryAsync<GetApartmentsDto>(query, new {ApartmentId = apartmentId});
    }
}
