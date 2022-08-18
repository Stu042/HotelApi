using Microsoft.EntityFrameworkCore;

using System;

namespace HotelApi.Models.Db {
	public class BookingModel : DbContext {
		public int Id { get; set; }
		public long Ref { get; set; }
		public int RoomId { get; set; }
		public int Visitor { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
