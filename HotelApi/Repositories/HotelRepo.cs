using HotelApi.Models;
using HotelApi.Models.Db;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelApi.Repositories;

public class HotelRepo : IHotelRepo {
	private readonly IDbContextFactory<HotelDbContext> _contextFactory;

	public HotelRepo(IDbContextFactory<HotelDbContext> contextFactory) {
		_contextFactory = contextFactory;
	}

	public string FetchHotelName(int id) {
		using (var context = _contextFactory.CreateDbContext()) {
			var result = context.Hotel.Where(hotel => hotel.Id == id).Select(h => h.Name).FirstOrDefault();
			return result;
		}
	}

	public HotelModel FetchHotel(string name) {
		name = name.ToLower();
		using (var context = _contextFactory.CreateDbContext()) {
			var hotel = context.Hotel.Where(hotel => hotel.Name.ToLower() == name).FirstOrDefault();
			if (hotel != default) {
				var rooms = context.Room.Where(room => room.HotelId == hotel.Id)
							.Select(room => new RoomModel {
								Id = room.Id,
								Capacity = room.Capacity,
								HotelId = room.HotelId,
								Number = room.Number,
								Style = room.Style
							}).ToArray();
				return new HotelModel {
					Id = hotel.Id,
					Name = hotel.Name,
					Rooms = rooms
				};
			}
		}
		return null;
	}

	public RoomModel[] FetchAvailableRooms(DateTime from, DateTime to, int guestCount) {
		using (var context = _contextFactory.CreateDbContext()) {
			var bookingsInRange = context.Booking.Where(b => !(to <= b.From || from >= b.To)).Select(booking => booking.RoomId);
			var freeRooms = context.Room.Where(r => r.Capacity >= guestCount && !bookingsInRange.Contains(r.Id))
							.Select(room => new RoomModel {
								Id = room.Id,
								Capacity = room.Capacity,
								HotelId = room.HotelId,
								Number = room.Number,
								Style = room.Style
							}).ToArray();
			return freeRooms;
		}
	}

	public int FetchRoomsMaxCapacity() {
		using (var context = _contextFactory.CreateDbContext()) {
			var roomCapacity = context.Room.Max(r => r.Capacity);
			return roomCapacity;
		}
	}

	public BookingModel FetchBooking(long bookingRef) {
		using (var context = _contextFactory.CreateDbContext()) {
			var result = context.Booking.Where(booking => booking.Ref == bookingRef).FirstOrDefault();
			return new BookingModel {
				Id = result.Id,
				From = result.From,
				To = result.To,
				GuestCount = result.GuestCount,
				Ref = result.Ref,
				RoomId = result.RoomId
			};
		}
	}

	public void SaveHotel(HotelModel model) {
		var row = new TblHotel {
			Id = model.Id,
			Name = model.Name,
		};
		using (var context = _contextFactory.CreateDbContext()) {
			context.Hotel.Add(row);
			context.SaveChanges();
		}
	}
	public void SaveRoom(RoomModel model) {
		var row = new TblRoom {
			Id = model.Id,
			Capacity = model.Capacity,
			HotelId = model.HotelId,
			Number = model.Number,
			Style = model.Style
		};
		using (var context = _contextFactory.CreateDbContext()) {
			context.Room.Add(row);
			context.SaveChanges();
		}
	}
	public void SaveBooking(BookingModel model) {
		var row = new TblBooking {
			Id = model.Id,
			RoomId = model.RoomId,
			Ref = model.Ref,
			From = model.From,
			To = model.To,
			GuestCount = model.GuestCount
		};
		using (var context = _contextFactory.CreateDbContext()) {
			context.Booking.Add(row);
			context.SaveChanges();
		}
	}

	public void TruncateDb() {
		using (var context = _contextFactory.CreateDbContext()) {
			var tableNames = context.Model.GetEntityTypes()
							.Select(t => t.GetTableName())
							.Distinct()
							.ToList();
			foreach (var tableName in tableNames) {
				context.Database.ExecuteSqlRaw($"SET FOREIGN_KEY_CHECKS = 0; TRUNCATE TABLE `{tableName}`;");
			}
		}
	}
}
