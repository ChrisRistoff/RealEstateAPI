using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models;

public class RealEstateContext : DbContext
{
    public DbSet<Area> areas { get; set; }
    public DbSet<Houses> houses { get; set; }
    public DbSet<Apartment> apartments { get; set; }

    public RealEstateContext(DbContextOptions<RealEstateContext> options)
        : base(options)
    {}
}
