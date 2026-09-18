using Microsoft.AspNetCore.Mvc;

namespace PersonenVerwaltung.App.UpdatePerson
{
	public class UpdatePersonEndpoint
	{
		public const string Pattern = "api/personen-verwaltung/v1/person/{personId}";


		public async static Task<IResult> HandleRequest(int personId, [FromBody] NameUpdateDto updateDto, [FromServices] IPersonService service, [FromServices] ILogger<UpdatePersonEndpoint> logger, HttpContext context)
		{
			try
			{
				PersonUpdate update = new PersonUpdate
				{
					FamilyName = updateDto.FamilyName,
					GivenName = updateDto.GivenName,
				};

				return await service.UpdatePersonAsync(personId, update, context.RequestAborted) switch
				{
					Person p => TypedResults.NoContent(),
					Exception err => err switch
					{
						ArgumentException aex => TypedResults.Problem(aex.Message, $"api/personen-verwaltung/v1/person/{personId}", 400, "BadRequest", "PATCH"),
						KeyNotFoundException nfex => TypedResults.Problem(nfex.Message, $"api/personen-verwaltung/v1/person/{personId}", 404, "NotFound", "PATCH"),
						UnauthorizedAccessException uaex => TypedResults.Problem(uaex.Message, $"api/personen-verwaltung/v1/person/{personId}", 403, "Forbidden", "PATCH"),
						_ => TypedResults.Problem(err.Message, $"api/personen-verwaltung/v1/person/{personId}", 500, "InternalServerError", "PATCH"),
					},
				};
			}
			catch ( Exception ex )
			{
				logger.LogError(ex, "[{Endpoint}]: Updating person '{PersonId}' failed with '{Error}'", nameof(UpdatePersonEndpoint), personId, ex.Message);

				return ex switch
				{
					_ => TypedResults.Problem(ex.Message, $"api/personen-verwaltung/v1/person/{personId}", 500, "InternalServerError", "PATCH"),
				};
			}
		}
	}
}
