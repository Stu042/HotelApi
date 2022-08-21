using HotelApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Repositories;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }
	public DbSet<TblHotel> Hotel { get; set; }
	public DbSet<TblRoom> Room { get; set; }
	public DbSet<TblBooking> Booking { get; set; }
}
