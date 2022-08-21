using Newtonsoft.Json;

namespace HotelApi.Models.Response {
	public class AvailableRoomsResponse {
		[JsonProperty("roomId")]
		public int RoomId { get; set; }
		[JsonProperty("hotelName")]
		public string HotelName { get; set; }
		[JsonProperty("style")]
		public string Style { get; set; }
		[JsonProperty("capacity")]
		public int Capacity { get; set; }
	}
}
