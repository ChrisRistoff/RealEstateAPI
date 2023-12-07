using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models;

public class Apartment
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

   [ForeignKey("areaId")]
   public Area areas { get; set; }
}
