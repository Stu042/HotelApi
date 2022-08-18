using HotelApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Repositories;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }
	public DbSet<HotelModel> Hotel { get; set; }
	public DbSet<RoomModel> Room { get; set; }
	public DbSet<BookingModel> Booking { get; set; }
}
