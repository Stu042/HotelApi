using System;

namespace HotelApi.Models {
	public class BookingModel {
		public long Id { get; set; }
		public long Ref { get; set; }
		public int RoomId { get; set; }
		public int GuestCount { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
