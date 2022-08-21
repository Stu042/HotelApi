using Newtonsoft.Json;

namespace HotelApi.Models.Response {
	public class FindHotelResponse {
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("rooms")]
		public FindHotelRoom[] Rooms { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
	}

	public class FindHotelRoom {
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("number")]
		public int Number { get; set; }
		[JsonProperty("style")]
		public string Style { get; set; }
		[JsonProperty("capacity")]
		public int Capacity { get; set; }
	}
}
