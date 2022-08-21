using HotelApi.Models;
using HotelApi.Models.Response;
using HotelApi.Repositories;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;
using FluentAssertions;


namespace Hotel.Tests {
	[TestClass]
	public class FindHotelTest {
		[TestMethod]
		public void TestForSuccess() {
			var request = CreateRequestAsync("GET", "http", "localhost:7071", "/api", "?name=Test Hotel").Result;
			var expected = new FindHotelResponse {
				Id = 1,
				Name = "Test Hotel",
				Rooms = new FindHotelRoom[1] {
					new FindHotelRoom {
						Capacity = 1,
						Id = 1,
						Number = 1,
						Style = "Single"
					}
				},
				Message = null
			};
			var repoReturns = new HotelModel {
				Id = 1,
				Name = "Test Hotel",
				Rooms = new RoomModel[1] {
					new RoomModel {
						Capacity = 1,
						HotelId = 1,
						Id = 1,
						Number = 1,
						Style = "Single"
					}
				}
			};
			var moqLogger = new Mock<ILogger<HotelApi.Hotel>>();
			var moqRepository = new Mock<IHotelRepo>();
			moqRepository.Setup(x => x.FetchHotel(It.IsAny<string>())).Returns(repoReturns);
			var hotel = new HotelApi.Hotel(moqRepository.Object, moqLogger.Object);
			var response = (OkObjectResult)hotel.FindHotel(request);
			var test = response.Value;
			response.Value.Should().BeEquivalentTo(expected);
		}

		private async Task<HttpRequest> CreateRequestAsync(string method, string scheme, string host, string path, string query) {
			var httpContext = new DefaultHttpContext();
			httpContext.Request.Method = method;
			httpContext.Request.Scheme = scheme;
			httpContext.Request.Host = new HostString(host);
			httpContext.Request.Path = new PathString(path);
			httpContext.Request.QueryString = new QueryString(query);
			httpContext.Request.ContentType = "application/json";
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			await writer.WriteAsync("{}");
			await writer.FlushAsync();
			stream.Position = 0;
			httpContext.Request.Body = stream;
			return httpContext.Request;
		}
	}
}
