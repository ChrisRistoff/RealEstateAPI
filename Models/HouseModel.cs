using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models;

public class Houses
{

    [Key]
    public int houseId { get; set; }
    public int areaId { get; set; }
    public DateTime postedOn { get; } = DateTime.Now;
    public string description { get; set; }
    public double price { get; set; }
    public string address { get; set; }
    public string postcode { get; set; }
    public int sqrFeet { get; set; }
    public int rooms { get; set; }
    public int bathrooms { get; set; }
    public int parkingSpaces { get; set; }
    public bool furnished { get; set; }

    [ForeignKey("areaId")]
    public Area areas { get; set; }
}


public class GetHousesDto
{

    [Key]
    public int houseId { get; set; }
    public int areaId { get; set; }
    public DateTime postedOn { get; } = DateTime.Now;
    public string description { get; set; }
    public double price { get; set; }
    public string address { get; set; }
    public string postcode { get; set; }
    public int sqrFeet { get; set; }
    public int rooms { get; set; }
    public int bathrooms { get; set; }
    public int parkingSpaces { get; set; }
    public bool furnished { get; set; }
}

public class CreateHouseDto
{
    [Required]
    [StringLength(1000), MinLength(20)]
    public string description { get; set; }
    [Required]
    public double price { get; set; }
    [Required]
    public string address { get; set; }
    [Required]
    public string postcode { get; set; }
    [Required]
    public int sqrFeet { get; set; }
    [Required]
    public int rooms { get; set; }
    [Required]
    public int bathrooms { get; set; }
    public int parkingSpaces { get; set; }
    public bool furnished { get; set; } = false;
}

public class UpdateHouseDto
{
    [Required]
    public double price { get; set; }
}
