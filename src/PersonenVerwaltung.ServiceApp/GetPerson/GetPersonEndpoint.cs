using Microsoft.AspNetCore.Mvc;

namespace PersonenVerwaltung.App.GetPerson
{
	public class GetPersonEndpoint
	{
		public const string Pattern = "api/personen-verwaltung/v1/person/{personId}";


		public async static Task<IResult> HandleRequest(int personId, [FromServices] IPersonService service, [FromServices] ILogger<GetPersonEndpoint> logger, HttpContext context)
		{
			try
			{
				return await service.GetPersonAsync(personId, context.RequestAborted) switch
				{
					Person p => TypedResults.Json(PersonDto.FromPerson(p)),
					Exception err => err switch
					{
						ArgumentException aex => TypedResults.Problem(aex.Message, $"api/personen-verwaltung/v1/person/{personId}", 400, "BadRequest", "GET"),
						KeyNotFoundException nfex => TypedResults.Problem(nfex.Message, $"api/personen-verwaltung/v1/person/{personId}", 404, "NotFound", "GET"),
						UnauthorizedAccessException uaex => TypedResults.Problem(uaex.Message, $"api/personen-verwaltung/v1/person/{personId}", 403, "Forbidden", "GET"),
						_ => TypedResults.Problem(err.Message, $"api/personen-verwaltung/v1/person/{personId}", 500, "InternalServerError", "GET"),
					},
				};
			}
			catch ( Exception ex )
			{
				logger.LogError(ex, "[{Endpoint}]: Getting person '{PersonId}' failed with '{Error}'", nameof(GetPersonEndpoint), personId, ex.Message);

				return ex switch
				{
					_ => TypedResults.Problem(ex.Message, $"api/personen-verwaltung/v1/person/{personId}", 500, "InternalServerError", "GET"),
				};
			}
		}
	}
}
