namespace HotelApi.Models {
	public class HotelModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public RoomModel[] Rooms { get; set; }
	}
}
