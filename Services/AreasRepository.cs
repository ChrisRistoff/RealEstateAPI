using Dapper;
using Npgsql;
using RealEstateAPI.Models;

namespace RealEstateAPI.Services;

public class AreasRepository(IConfiguration configuration)
{
    public async Task<IEnumerable<GetAllAreasDto>> GetAllAreasAsync()
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        string query = "SELECT a.\"areaId\", a.name, a.description, a.schools, a.shops, a.kindergardens, " +
                       "COUNT(DISTINCT h.\"houseId\") as houseC, " +
                       "COUNT(DISTINCT ap.\"apartmentId\") as apartmentC " +
                       "FROM areas a " +
                       "LEFT JOIN houses h ON h.\"areaId\" = a.\"areaId\" " +
                       "LEFT JOIN apartments ap ON ap.\"areaId\" = a.\"areaId\" " +
                       "GROUP BY a.\"areaId\", a.name, a.description, a.schools, a.shops, a.kindergardens;";

        return await connection.QueryAsync<GetAllAreasDto>(query);
    }
}
