using HotelApi.Models;

using System;

namespace HotelApi.Repositories;

public interface IHotelRepo {
	string FetchHotelName(int id);
	HotelModel FetchHotel(string name);
	RoomModel[] FetchAvailableRooms(DateTime from, DateTime to, int guestCount);
	int FetchRoomsMaxCapacity();
	BookingModel FetchBooking(long bookingRef);
	void SaveHotel(HotelModel model);
	void SaveRoom(RoomModel model);
	void SaveBooking(BookingModel model);
	void TruncateDb();
}
