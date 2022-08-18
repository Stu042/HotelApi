﻿using Newtonsoft.Json;

namespace HotelApi.Models.Response {
	public class FindHotelResponse {
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
	}
}