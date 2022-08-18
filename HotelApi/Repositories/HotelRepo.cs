using HotelApi.Models.Db;

using System;
using System.Linq;


namespace HotelApi.Repositories;

public class HotelRepo {
	private readonly HotelDbContext _context;
	public HotelRepo(HotelDbContext context) {
		_context = context;
	}

	public string FetchHotelName(int id) {
		var result = _context.Hotel.Where(hotel => hotel.Id == id).Select(h => h.Name).FirstOrDefault();
		return result;
	}

	public HotelModel FetchHotel(string name) {
		name = name.ToLower();
		var result = _context.Hotel.Where(hotel => hotel.Name.ToLower() == name).FirstOrDefault();
		return result;
	}

	public RoomModel[] FetchAvailableRooms(DateTime from, DateTime to, int guestCount) {
		var allBookings = _context.Booking.Select(b => (b.To < from || b.From > to) ? b.RoomId : 0);
		var freeRooms = _context.Room.Where(r => r.Capacity >= guestCount && !allBookings.Contains(r.Id)).ToArray();
		return freeRooms;
	}

	public BookingModel FetchBooking(long bookingRef) {
		var result = _context.Booking.Where(booking => booking.Ref == bookingRef).FirstOrDefault();
		return result;
	}
}
