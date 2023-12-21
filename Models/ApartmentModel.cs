using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Models;

public class Apartment
{

    [Key]
    public int apartmentId { get; set; }
    public int areaId { get; set; }
    public DateTime postedOn { get; } = DateTime.Now;
    [NotNull]
    public string description { get; set; }
    [NotNull]
    public double price { get; set; } = 0;
    [NotNull]
    public string address { get; set; }
    [NotNull]
    public string postcode { get; set; }
    [NotNull]
    public int sqrFeet { get; set; }
    [NotNull]
    public int rooms { get; set; }
    [NotNull]
    public int bathrooms { get; set; }
    [NotNull]
    public int parkingSpaces { get; set; }
    public bool furnished { get; set; } = false;

   [ForeignKey("areaId")]
   public Area areas { get; set; }
}

public class GetApartmentsDto
{

    [Key]
    public int apartmentId { get; set; }
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

public class CreateApartmentDto
{
    [Required]
    [StringLength(20)]
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

public class UpdateApartmentDto
{
    [Required]
    public double price { get; set; }
}
