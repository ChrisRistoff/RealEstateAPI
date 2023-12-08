using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models;

public class Area
{
    [Key]
    public int areaId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string schools { get; set; }
    public string shops { get; set; }
    public string kindergardens { get; set; }
}

public class GetAllAreasDto
{
    public int areaId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string schools { get; set; }
    public string shops { get; set; }
    public string kindergardens { get; set; }
    public int houseC { get; set; }
    public int apartmentC { get; set; }
}
