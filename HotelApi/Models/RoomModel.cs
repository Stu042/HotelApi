namespace HotelApi.Models {
	public class RoomModel {
		public int Id { get; set; }
		public int Number { get; set; }
		public string Style { get; set; }
		public int Capacity { get; set; }
		public int HotelId { get; set; }
	}

	public class RoomAndHotelNameModel {
		public int Id { get; set; }
		public int Number { get; set; }
		public string Style { get; set; }
		public int Capacity { get; set; }
		public int HotelId { get; set; }
		public string HotelName { get; set; }

	}
}
