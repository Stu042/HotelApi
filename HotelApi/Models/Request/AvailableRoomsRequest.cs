using Newtonsoft.Json;

using System;

namespace HotelApi.Models.Request {
	public class AvailableRoomsRequest {
		[JsonProperty("fromYear")]
		public int FromYear { get; set; }
		[JsonProperty("fromMonth")]
		public int FromMonth { get; set; }
		[JsonProperty("fromDay")]
		public int FromDay { get; set; }
		[JsonProperty("toYear")]
		public int ToYear { get; set; }
		[JsonProperty("toMonth")]
		public int ToMonth { get; set; }
		[JsonProperty("toDay")]
		public int ToDay { get; set; }
		[JsonProperty("guestCount")]
		public int GuestCount { get; set; }
	}

	public class AvailableRoomsRequestParsed {
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public int GuestCount { get; set; }
		public string Message { get; set; }
	}
}
