using System.Text;

using Microsoft.Extensions.Logging;

namespace PersonenVerwaltung.ServiceClient
{
	public class PersonenVerwaltungClient
	{
		public PersonenVerwaltungClient(IHttpClientFactory factory, ILogger<PersonenVerwaltungClient> logger)
		{
			_client = factory.CreateClient(nameof(PersonenVerwaltungClient));
			_logger = logger;
		}

		public PersonenVerwaltungClient(HttpClient client, ILogger<PersonenVerwaltungClient> logger)
		{
			_client = client;
			_logger = logger;
		}


		public async Task<ApiResult<PersonDto>> GetPersonAsync(int id, CancellationToken cancellationToken = default)
		{
			try
			{
				var responseMessage = await _client.GetAsync($"person/{id}", cancellationToken);

				if ( !responseMessage.IsSuccessStatusCode )
					return await responseMessage.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);

				return await responseMessage.Content.ReadFromJsonAsync<PersonDto>(cancellationToken);
			}
			catch ( Exception ex )
			{
				_logger.LogError(ex, "[{ApiClientType}]: GetPersonAsync for '{PersonId}' failed with '{ErrorMessage}'", nameof(PersonenVerwaltungClient), id, ex.Message);

				return ex;
			}
		}

		public async Task<ApiResult<Page<PersonDto>>> ListPersonsAsync(string? filter = null, int page = 0, int pagesize = 20, CancellationToken cancellationToken = default)
		{
			bool firstSeparator = true;
			StringBuilder path = new StringBuilder();

			if ( pagesize < 1 )
				pagesize = 1;

			if ( pagesize > 100 )
				pagesize = 100;

			path.Append("personen");

			if ( !string.IsNullOrWhiteSpace(filter) )
			{
				if ( firstSeparator )
					path.Append("?filter=").Append(filter);
				else
					path.Append("&filter=").Append(filter);

				firstSeparator = false;
			}

			if ( page > 0 )
			{
				if ( firstSeparator )
					path.Append("?page=").Append(page);
				else
					path.Append("&page=").Append(page);

				firstSeparator = false;
			}

			if ( pagesize != 20 )
			{
				if ( firstSeparator )
					path.Append("?pagesize=").Append(pagesize);
				else
					path.Append("&pagesize=").Append(pagesize);

				firstSeparator = false;
			}

			try
			{
				var responseMessage = await _client.GetAsync(path.ToString(), cancellationToken);

				if ( !responseMessage.IsSuccessStatusCode )
					return await responseMessage.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);

				return await responseMessage.Content.ReadFromJsonAsync<Page<PersonDto>>(cancellationToken);
			}
			catch ( Exception ex )
			{
				_logger.LogError(ex, "[{ApiClientType}]: ListPersonsAsync for path '{RequestPath}' failed with '{ErrorMessage}'", nameof(PersonenVerwaltungClient), path, ex.Message);

				return ex;
			}
		}

		public async Task<ApiResult> UpdatePersonAsync(int id, NameUpdateDto nameUpdate, CancellationToken cancellationToken = default)
		{
			if ( nameUpdate.FamilyName == null && nameUpdate.GivenName == null )
				return new NoContent { Status = System.Net.HttpStatusCode.NoContent };

			try
			{
				var responseMessage = await _client.PatchAsync($"person/{id}", JsonContent.Create(nameUpdate), cancellationToken);

				if ( !responseMessage.IsSuccessStatusCode )
					return await responseMessage.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);

				return new NoContent { Status = responseMessage.StatusCode };
			}
			catch ( Exception ex )
			{
				_logger.LogError(ex, "[{ApiClientType}]: UpdatePersonAsync for '{PersonId}' failed with '{ErrorMessage}'", nameof(PersonenVerwaltungClient), id, ex.Message);

				return ex;
			}
		}


		readonly HttpClient
			_client;

		readonly ILogger<PersonenVerwaltungClient>
			_logger;
	}

	public readonly struct PersonDto
	{
		public required int Id
		{ get; init; }

		public IEnumerable<AddressDto> Addresses
		{ get; init; }

		public required DateOnly Birthdate
		{ get; init; }

		public required string FamilyName
		{ get; init; }

		public required string GivenName
		{ get; init; }

		public IEnumerable<PhoneDto> Phones
		{ get; init; }
	}

	public readonly struct AddressDto
	{
		public required string City
		{ get; init; }

		public required string StreetNumber
		{ get; init; }

		public required string Street
		{ get; init; }

		public required string Zipcode
		{ get; init; }
	}

	public readonly struct PhoneDto
	{
		public required string Number
		{ get; init; }
	}

	public readonly struct NameUpdateDto
	{
		public string? FamilyName
		{ get; init; }

		public string? GivenName
		{ get; init; }
	}
}
