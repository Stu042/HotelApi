using HotelApi.Models.Request;
using HotelApi.Models.Response;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotelApi;
public class Hotel {
	const string FIND_HOTEL = "FindHotel";
	const string BOOK_ROOM = "BookRoom";
	const string PARAMETER_NAME = "name";


	[FunctionName(FIND_HOTEL)]
	[OpenApiOperation(operationId: FIND_HOTEL, tags: new[] { "Hotel" })]
	[OpenApiParameter(name: PARAMETER_NAME, In = Microsoft.OpenApi.Models.ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Name of the Hotel to search for.")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(FindHotelResponse))]
	public async Task<IActionResult> FindHotel([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ILogger log) {
		var request = ParseFindHotelRequest(req, log);
		if (!string.IsNullOrWhiteSpace(request.Message)) {
			return new BadRequestObjectResult(new { message = request.Message });
		}
		var respsonse = new FindHotelResponse {
			Name = "Hotel Stu"
		};
		return new OkObjectResult(respsonse);
	}



	private FindHotelRequest ParseFindHotelRequest(HttpRequest req, ILogger log) {
		FindHotelRequest result;
		if (req.Query.ContainsKey(PARAMETER_NAME)) {
			result = new FindHotelRequest {
				Name = req.Query[PARAMETER_NAME]
			};
			log.LogDebug($"Found '{PARAMETER_NAME}' parameter, {result.Name}");
		} else {
			log.LogError($"Missing '{PARAMETER_NAME}' parameter found");
			result = new FindHotelRequest {
				Message = $"'{PARAMETER_NAME}' parameter is missing."
			};
		}
		return result;
	}




	[FunctionName(BOOK_ROOM)]
	[Produces("application/json")]
	public async Task<IActionResult> BookRoom([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req, ILogger log) {
		return new OkObjectResult(0);
	}
}
