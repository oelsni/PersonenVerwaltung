using System.Linq.Expressions;

using Microsoft.AspNetCore.Mvc;

namespace PersonenVerwaltung.App.ListPersons
{
	public class ListPersonsEndpoint
	{
		public const string Pattern = "api/personen-verwaltung/v1/personen";


		public async static Task<IResult> HandleRequest([FromServices] IPersonService service, [FromServices] ILogger<ListPersonsEndpoint> logger, HttpContext context)
		{
			int pageNr = 0;
			int pagesize = 20;
			string? filter = null;

			try
			{
				// Check if a filter for name is provided. Also check that it is exactly 1 filter (multiple filters are not supported and will be ignored)
				if ( context.Request.Query.ContainsKey("filter") && context.Request.Query["filter"].Count == 1 )
					filter = context.Request.Query["filter"][0];

				// Check if a page is provided. Also check that the provided page is valid (page >= 0)
				if ( context.Request.Query.ContainsKey("page")
					&& context.Request.Query["page"].Count == 1
					&& int.TryParse(context.Request.Query["page"][0], out int p)
					&& p >= 0 )
					pageNr = p;

				// Check if a pagesize is provided. Also check that the provided pagesize is valid (1 <= pagesize <= 100)
				if ( context.Request.Query.ContainsKey("pagesize")
					&& context.Request.Query["pagesize"].Count == 1
					&& int.TryParse(context.Request.Query["pagesize"][0], out int ps)
					&& ps >= 1 && ps <= 100 )
					pagesize = ps;

				filter = GetNormalizedFilterAndSearchMethod(filter, out SearchMethod method);

				Result<PersonPage> result;

				if ( method != SearchMethod.None )
				{
					Expression<Func<Person, bool>> predicate;

					// Filter gets applied to FamilyName as well as GivenName. Also the search is done CaseInsensitive
					if ( method == SearchMethod.Contains )
						predicate = e => e.FamilyName.Contains(filter) || e.GivenName.Contains(filter);
					else if ( method == SearchMethod.EndsWith )
						predicate = e => e.FamilyName.EndsWith(filter) || e.GivenName.EndsWith(filter);
					else if ( method == SearchMethod.StartsWith )
						predicate = e => e.FamilyName.StartsWith(filter) || e.GivenName.StartsWith(filter);
					else
						predicate = e => e.FamilyName.Equals(filter) || e.GivenName.Equals(filter);

					result = await service.FilterPersonsAsync(predicate, pageNr, pagesize, context.RequestAborted);
				}
				else
				{
					// No filter provided so persons are loaded without restriction
					result = await service.ListPersonsAsync(pageNr, pagesize, context.RequestAborted);
				}

				return result switch
				{
					PersonPage page => TypedResults.Json(new PageDto<PersonDto> { Items = page.Persons.Select(PersonDto.FromPerson), TotalCount = page.TotalCount }),
					Exception err => err switch
					{
						ArgumentException aex => TypedResults.Problem(aex.Message, "api/personen-verwaltung/v1/personen", 400, "BadRequest", "GET"),
						KeyNotFoundException nfex => TypedResults.Problem(nfex.Message, "api/personen-verwaltung/v1/personen", 404, "NotFound", "GET"),
						UnauthorizedAccessException uaex => TypedResults.Problem(uaex.Message, "api/personen-verwaltung/v1/personen", 403, "Forbidden", "GET"),
						_ => TypedResults.Problem(err.Message, "api/personen-verwaltung/v1/personen", 500, "InternalServerError", "GET"),
					},
				};
			}
			catch ( Exception ex )
			{
				logger.LogError(ex, "[{Endpoint}]: Listing persons failed with '{Error}' (Filter: {Filter})", nameof(ListPersonsEndpoint), ex.Message, filter);

				return ex switch
				{
					_ => TypedResults.Problem(ex.Message, "api/personen-verwaltung/v1/personen", 500, "InternalServerError", "GET"),
				};
			}
		}


		private static string GetNormalizedFilterAndSearchMethod(string? filter, out SearchMethod method)
		{
			method = SearchMethod.None;

			if ( string.IsNullOrWhiteSpace(filter) )
				return "";

			var normalized = filter.AsSpan().Trim();

			// Filter supports simple wildcards (Contains '*xxx*' / EndsWith '*xxx' / StartsWith 'xxx*' / Equals 'xxx')
			if ( normalized[0] == '*' && normalized[normalized.Length - 1] == '*' )
			{
				normalized = normalized.Slice(1, normalized.Length - 2).Trim();

				if ( !normalized.IsEmpty )
					method = SearchMethod.Contains;
			}
			else if ( normalized[0] == '*' )
			{
				normalized = normalized.Slice(1).Trim();

				if ( !normalized.IsEmpty )
					method = SearchMethod.EndsWith;
			}
			else if ( normalized[filter.Length - 1] == '*' )
			{
				normalized = normalized.Slice(0, normalized.Length - 1).Trim();

				if ( !normalized.IsEmpty )
					method = SearchMethod.StartsWith;
			}
			else
			{
				method = SearchMethod.Equals;
			}

			return normalized.IsEmpty ? "" : normalized.ToString();
		}

		enum SearchMethod
		{
			None,
			Contains,
			StartsWith,
			EndsWith,
			Equals,
		}
	}
}
