using Newtonsoft.Json;

namespace HotelApi.Models.Response {
	public class ErrorResponse {
		[JsonProperty("message")]
		public string Message { get; set; }
	}
}
