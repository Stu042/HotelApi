using Newtonsoft.Json;

namespace HotelApi.Models.Request {
	public class FindHotelRequest {

		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
	}
}
