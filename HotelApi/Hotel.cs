using HotelApi.Models.Request;
using HotelApi.Models.Response;
using HotelApi.Repositories;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

using System;
using System.Net;
using System.Linq;


namespace HotelApi;
public class Hotel {
	const string FIND_HOTEL = "FindHotel";
	const string AVAILABLE_ROOMS = "AvailableRooms";
	const string FIND_HOTEL_PARAMETER_NAME = "name";
	const string AVAIL_ROOM_PARAMETER_FROM_YEAR = "fromYear";
	const string AVAIL_ROOM_PARAMETER_FROM_MONTH = "fromMonth";
	const string AVAIL_ROOM_PARAMETER_FROM_DAY = "fromDay";
	const string AVAIL_ROOM_PARAMETER_TO_YEAR = "toYear";
	const string AVAIL_ROOM_PARAMETER_TO_MONTH = "toMonth";
	const string AVAIL_ROOM_PARAMETER_TO_DAY = "toDay";
	const string AVAIL_ROOM_PARAMETER_GUEST_COUNT = "guestCount";

	private readonly IHotelRepo _hotelRepo;
	private readonly ILogger<Hotel> _log;


	public Hotel(IHotelRepo hotelRepo, ILogger<Hotel> log) {
		_hotelRepo = hotelRepo;
		_log = log;
	}

	[FunctionName(FIND_HOTEL)]
	[OpenApiOperation(operationId: FIND_HOTEL, tags: new[] { "Hotel" })]
	[OpenApiParameter(name: FIND_HOTEL_PARAMETER_NAME, In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Name of the Hotel to search for.")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(FindHotelResponse))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(ErrorResponse))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ErrorResponse))]
	public IActionResult FindHotel([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req) {
		var request = ParseFindHotelRequest(req);
		if (!string.IsNullOrWhiteSpace(request.Message)) {
			return new BadRequestObjectResult(new ErrorResponse { Message = request.Message });
		}
		var hotel = _hotelRepo.FetchHotel(request.Name);
		if (hotel == default) {
			return new NotFoundObjectResult(new ErrorResponse { Message = $"Unable to find hotel named: {request.Name}" });
		}
		_log.LogDebug($"Found Hotel, ID: {hotel.Id}, Name: {hotel.Name}");
		var respsonse = new FindHotelResponse {
			Id = hotel.Id,
			Name = hotel.Name,
			Rooms = hotel.Rooms.Select(room => new FindHotelRoom {
				Id = room.Id,
				Number = room.Number,
				Capacity = room.Capacity,
				Style = room.Type
			}).ToArray()
		};
		return new OkObjectResult(respsonse);
	}


	private FindHotelRequest ParseFindHotelRequest(HttpRequest req) {
		FindHotelRequest result;
		if (req.Query.ContainsKey(FIND_HOTEL_PARAMETER_NAME)) {
			result = new FindHotelRequest {
				Name = req.Query[FIND_HOTEL_PARAMETER_NAME]
			};
		} else {
			_log.LogError($"Missing '{FIND_HOTEL_PARAMETER_NAME}' parameter found");
			result = new FindHotelRequest {
				Message = $"'{FIND_HOTEL_PARAMETER_NAME}' parameter is missing."
			};
		}
		return result;
	}


	[FunctionName(AVAILABLE_ROOMS)]
	[OpenApiOperation(operationId: AVAILABLE_ROOMS, tags: new[] { "Hotel" })]
	[OpenApiParameter(name: AVAIL_ROOM_PARAMETER_FROM_YEAR, In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Start year for request.")]
	[OpenApiParameter(name: AVAIL_ROOM_PARAMETER_FROM_MONTH, In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Start month for request.")]
	[OpenApiParameter(name: AVAIL_ROOM_PARAMETER_FROM_DAY, In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Start day for request.")]
	[OpenApiParameter(name: AVAIL_ROOM_PARAMETER_TO_YEAR, In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(int), Description = "End year for request.")]
	[OpenApiParameter(name: AVAIL_ROOM_PARAMETER_TO_MONTH, In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(int), Description = "End month for request.")]
	[OpenApiParameter(name: AVAIL_ROOM_PARAMETER_TO_DAY, In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(int), Description = "End day for request.")]
	[OpenApiParameter(name: AVAIL_ROOM_PARAMETER_GUEST_COUNT, In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Number of guests to check for.")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AvailableRoomsResponse[]))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(ErrorResponse))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ErrorResponse))]
	public IActionResult AvailableRooms([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req) {
		var request = ParseAvailableRoomsRequest(req);
		if (!string.IsNullOrWhiteSpace(request.Message)) {
			return new BadRequestObjectResult(new ErrorResponse { Message = request.Message });
		}
		if (request.From >= request.To) {
			_log.LogError("Dates are invalid, 'to' date must be later than 'from' date.");
			return new BadRequestObjectResult(new ErrorResponse { Message = "Dates are invalid, 'to' date needs to be after 'from' date." });
		}
		var maxCapacity = _hotelRepo.FetchRoomsMaxCapacity();
		if (request.GuestCount > maxCapacity) {
			return new NotFoundObjectResult(new ErrorResponse { Message = $"No rooms have a capacity over {maxCapacity}." });
		}
		var availRoomModels = _hotelRepo.FetchAvailableRooms(request.From, request.To, request.GuestCount);
		if (availRoomModels.Count() == 0) {
			return new NotFoundObjectResult(new ErrorResponse { Message = "Unable to find any rooms for those dates at that capacity." });
		}
		var availRooms = availRoomModels.Select(ar => new AvailableRoomsResponse {
			RoomId = ar.Id,
			HotelName = _hotelRepo.FetchHotelName(ar.HotelId),	// gotta be a nicer way to do this
			Style = ar.Type,
			Capacity = ar.Capacity
		}).ToArray();
		return new OkObjectResult(availRooms);
	}


	private AvailableRoomsRequestParsed ParseAvailableRoomsRequest(HttpRequest req) {
		var result = new AvailableRoomsRequestParsed();
		if (req.Query.ContainsKey(AVAIL_ROOM_PARAMETER_FROM_YEAR) &&
			req.Query.ContainsKey(AVAIL_ROOM_PARAMETER_FROM_MONTH) &&
			req.Query.ContainsKey(AVAIL_ROOM_PARAMETER_FROM_DAY)) {
			var from = ParseDate(req.Query[AVAIL_ROOM_PARAMETER_FROM_YEAR], req.Query[AVAIL_ROOM_PARAMETER_FROM_MONTH], req.Query[AVAIL_ROOM_PARAMETER_FROM_DAY]);
			if (from != null) {
				result.From = (DateTime)from;
			}
		}
		if (result.From == default) {
			_log.LogError("Unable to parse From date.");
			result.Message = "Unable to parse 'from' date.";
			return result;
		}
		if (req.Query.ContainsKey(AVAIL_ROOM_PARAMETER_TO_YEAR) &&
			req.Query.ContainsKey(AVAIL_ROOM_PARAMETER_TO_MONTH) &&
			req.Query.ContainsKey(AVAIL_ROOM_PARAMETER_TO_DAY)) {
			var to = ParseDate(req.Query[AVAIL_ROOM_PARAMETER_TO_YEAR], req.Query[AVAIL_ROOM_PARAMETER_TO_MONTH], req.Query[AVAIL_ROOM_PARAMETER_TO_DAY]);
			if (to != null) {
				result.To = (DateTime)to;
			}
		}
		if (result.To == default) {
			_log.LogError("Unable to parse To date.");
			result.Message = "Unable to parse 'to' date.";
			return result;
		}
		if (req.Query.ContainsKey(AVAIL_ROOM_PARAMETER_GUEST_COUNT)) {
			try {
				result.GuestCount = int.Parse(req.Query[AVAIL_ROOM_PARAMETER_GUEST_COUNT]);
			} catch (Exception ex) {
				_log.LogError($"Unable to parse GuestCount: {AVAIL_ROOM_PARAMETER_GUEST_COUNT}, reason: {ex.Message}");
				result.Message = $"Unable to parse {AVAIL_ROOM_PARAMETER_GUEST_COUNT}.";
				return result;
			}
		}
		return result;
	}

	private DateTime? ParseDate(string year, string month, string day) {
		try {
			int yearInt = int.Parse(year);
			int monthInt = int.Parse(month);
			int dayInt = int.Parse(day);
			return new DateTime(yearInt, monthInt, dayInt);
		} catch(Exception ex) {
			_log.LogError($"Unable to parse date, year: {year}, month: {month}, day: {day}, reason: {ex.Message}");
			return null;
		}
	}
}
