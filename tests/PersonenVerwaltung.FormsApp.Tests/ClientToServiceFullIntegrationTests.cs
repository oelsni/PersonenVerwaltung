using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PersonenVerwaltung.App.PersonServiceImpl;
using PersonenVerwaltung.ServiceClient;

using Testcontainers.MsSql;

namespace PersonenVerwaltung.App.Tests
{
	[TestClass]
	public sealed class ClientToServiceFullIntegrationTests
	{
		[ClassInitialize]
		public static async Task ClassInit(TestContext context)
		{
			_container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04").Build();
			await _container.StartAsync();

			_factory = new TestWebApplicationFactory<Program>(_container.GetConnectionString());
			
			var httpClient = _factory!.CreateClient();
			httpClient.BaseAddress = new Uri(httpClient.BaseAddress!, "api/personen-verwaltung/v1/");

			_client = new PersonenVerwaltungClient(httpClient, _factory.Services.GetRequiredService<ILogger<PersonenVerwaltungClient>>());

			await using var scoped = _factory.Services.CreateAsyncScope();

			PersonalVerwaltungContext dbContext = scoped.ServiceProvider.GetRequiredService<PersonalVerwaltungContext>();

			await dbContext.Database.ExecuteSqlRawAsync((await File.ReadAllTextAsync("CreateTables.sql"))
				.Replace("GO", "")
				.Replace("USE [PersonenVerwaltung]", "USE master"));

			await dbContext.Database.ExecuteSqlRawAsync((await File.ReadAllTextAsync("SeedTables.sql"))
				.Replace("GO", "")
				.Replace("USE [PersonenVerwaltung]", "USE master"));
		}

		[ClassCleanup]
		public static async Task ClassCleanup()
		{
			_factory?.Dispose();
			await (_container?.StopAsync() ?? Task.CompletedTask);
			await (_container?.DisposeAsync() ?? ValueTask.CompletedTask);
		}


		[TestMethod]
		public async Task GetPersonAsync_ShouldReturnPerson_WhenPersonIdIsValid()
		{
			var client = CreateApiClient();

			var person = await client.GetPersonAsync(1);

			if ( person switch { ServiceClient.PersonDto p => false, _ => true } )
				Assert.Fail("A PersonDto object was expected but was not there.");
		}

		[TestMethod]
		public async Task GetPersonAsync_ShouldReturnProblemDetailsNotFound_WhenPersonIdDoesNotExist()
		{
			var client = CreateApiClient();

			var person = await client.GetPersonAsync(251);

			if ( person switch { ProblemDetails p => !(p.Status == System.Net.HttpStatusCode.NotFound), _ => true } )
				Assert.Fail("A ProblemDetails object with status 404-NotFound was expected but was not there.");
		}

		[TestMethod]
		public async Task ListPersonsAsync_ShouldReturnPageWithTotalCount250AndContaining20PersonsFromIdOneTo20_WhenNoPagingInformationIsProvided()
		{
			var client = CreateApiClient();

			var persons = await client.ListPersonsAsync();

			Page<ServiceClient.PersonDto> page = persons switch { Page<ServiceClient.PersonDto> p => p, _ => default };

			if ( page.TotalCount == 0 )
				Assert.Fail("A Page<ServiceClient.PersonDto> object was expected but was not there.");

			Assert.AreEqual(250, page.TotalCount);
			Assert.HasCount(20, page.Items);

			HashSet<int> expectedIds = Enumerable.Range(1, 20).ToHashSet();
			HashSet<int> actualIds = page.Items.Select(e => e.Id).ToHashSet();

			Assert.AreEqual(expectedIds.Count, actualIds.Count);

			expectedIds.ExceptWith(actualIds);

			Assert.IsEmpty(expectedIds);
		}

		[TestMethod]
		public async Task ListPersonsAsync_ShouldReturnPageWithTotalCount250AndContaining20PersonsFromId61To80_WhenPageNumber3IsProvided()
		{
			var client = CreateApiClient();

			var persons = await client.ListPersonsAsync(page: 3);

			Page<ServiceClient.PersonDto> page = persons switch { Page<ServiceClient.PersonDto> p => p, _ => default };

			if ( page.TotalCount == 0 )
				Assert.Fail("A Page<ServiceClient.PersonDto> object was expected but was not there.");

			Assert.AreEqual(250, page.TotalCount);
			Assert.HasCount(20, page.Items);

			HashSet<int> expectedIds = Enumerable.Range(61, 20).ToHashSet();
			HashSet<int> actualIds = page.Items.Select(e => e.Id).ToHashSet();

			Assert.AreEqual(expectedIds.Count, actualIds.Count);

			expectedIds.ExceptWith(actualIds);

			Assert.IsEmpty(expectedIds);
		}

		[TestMethod]
		public async Task ListPersonsAsync_ShouldReturnPageWithTotalCount250AndContaining50PersonsFromId101To150_WhenPageNumber2AndPagesize50IsProvided()
		{
			var client = CreateApiClient();

			var persons = await client.ListPersonsAsync(page: 2, pagesize: 50);

			Page<ServiceClient.PersonDto> page = persons switch { Page<ServiceClient.PersonDto> p => p, _ => default };

			if ( page.TotalCount == 0 )
				Assert.Fail("A Page<ServiceClient.PersonDto> object was expected but was not there.");

			Assert.AreEqual(250, page.TotalCount);
			Assert.HasCount(50, page.Items);

			HashSet<int> expectedIds = Enumerable.Range(101, 50).ToHashSet();
			HashSet<int> actualIds = page.Items.Select(e => e.Id).ToHashSet();

			Assert.AreEqual(expectedIds.Count, actualIds.Count);

			expectedIds.ExceptWith(actualIds);

			Assert.IsEmpty(expectedIds);
		}

		[TestMethod]
		public async Task ListPersonsAsync_ShouldReturnPageWithTotalCount250AndContaining10PersonsFromId241To250_WhenPageNumber12IsProvided()
		{
			var client = CreateApiClient();

			var persons = await client.ListPersonsAsync(page: 12);

			Page<ServiceClient.PersonDto> page = persons switch { Page<ServiceClient.PersonDto> p => p, _ => default };

			if ( page.TotalCount == 0 )
				Assert.Fail("A Page<ServiceClient.PersonDto> object was expected but was not there.");

			Assert.AreEqual(250, page.TotalCount);
			Assert.HasCount(10, page.Items);

			HashSet<int> expectedIds = Enumerable.Range(241, 10).ToHashSet();
			HashSet<int> actualIds = page.Items.Select(e => e.Id).ToHashSet();

			Assert.AreEqual(expectedIds.Count, actualIds.Count);

			expectedIds.ExceptWith(actualIds);

			Assert.IsEmpty(expectedIds);
		}

		[TestMethod]
		public async Task ListPersonsAsync_ShouldReturnPageWithTotalCount250AndContainingZeroPersons_WhenPageNumber13IsProvided()
		{
			var client = CreateApiClient();

			var persons = await client.ListPersonsAsync(page: 13);

			Page<ServiceClient.PersonDto> page = persons switch { Page<ServiceClient.PersonDto> p => p, _ => default };

			if ( page.TotalCount == 0 )
				Assert.Fail("A Page<ServiceClient.PersonDto> object was expected but was not there.");

			Assert.AreEqual(250, page.TotalCount);
			Assert.HasCount(0, page.Items);
		}

		[TestMethod]
		public async Task ListPersonsAsync_ShouldReturnPageWithTotalCount13AndContaining10Persons_WhenPageNumber0AndPagesize10AndFilter_Contains_as_IsProvided()
		{
			var client = CreateApiClient();

			var persons = await client.ListPersonsAsync(filter: "*as*", page: 0, pagesize: 10);

			Page<ServiceClient.PersonDto> page = persons switch { Page<ServiceClient.PersonDto> p => p, _ => default };

			if ( page.TotalCount == 0 )
				Assert.Fail("A Page<ServiceClient.PersonDto> object was expected but was not there.");

			Assert.AreEqual(13, page.TotalCount);
			Assert.HasCount(10, page.Items);
		}

		[TestMethod]
		public async Task ListPersonsAsync_ShouldReturnPageWithTotalCount13AndContaining3Persons_WhenPageNumber1AndPagesize10AndFilter_Contains_as_IsProvided()
		{
			var client = CreateApiClient();

			var persons = await client.ListPersonsAsync(filter: "*as*", page: 1, pagesize: 10);

			Page<ServiceClient.PersonDto> page = persons switch { Page<ServiceClient.PersonDto> p => p, _ => default };

			if ( page.TotalCount == 0 )
				Assert.Fail("A Page<ServiceClient.PersonDto> object was expected but was not there.");

			Assert.AreEqual(13, page.TotalCount);
			Assert.HasCount(3, page.Items);
		}

		[TestMethod]
		public async Task UpdatePersonAsync_ShouldReturnNoContent_WhenNameUpdateAndPersonIdIsValid()
		{
			var client = CreateApiClient();
			var result = await client.GetPersonAsync(1);

			ServiceClient.PersonDto person1 = result switch { ServiceClient.PersonDto p => p, _ => default };

			if ( person1.Id == 0 )
				Assert.Fail("result was expected to be a PersonDto object but was not there.");

			ServiceClient.PersonDto person2 = person1;

			while ( person1.FamilyName == person2.FamilyName )
			{
				result = await client.GetPersonAsync(2);
				person2 = result switch { ServiceClient.PersonDto p => p, _ => default };

				if ( person2.Id == 0 )
					Assert.Fail("result was expected to be a PersonDto object but was not there.");
			}

			NameUpdateDto update = new NameUpdateDto { FamilyName = person2.FamilyName };
			var updateResult = await client.UpdatePersonAsync(1, update);

			if ( updateResult switch { NoContent p => false, _ => true } )
				Assert.Fail("updateResult was expected to be a NoContent object but was not there.");

			result = await client.GetPersonAsync(1);

			ServiceClient.PersonDto updatedPerson = result switch { ServiceClient.PersonDto p => p, _ => default };

			if ( updatedPerson.Id == 0 )
				Assert.Fail("result was expected to be a PersonDto object but was not there.");

			Assert.AreEqual(update.FamilyName, updatedPerson.FamilyName);
		}

		[TestMethod]
		public async Task UpdatePersonAsync_ShouldReturnProblemDetailWithNotFound_WhenPersonIdIsInvalid()
		{
			var client = CreateApiClient();
			var result = await client.GetPersonAsync(1);

			NameUpdateDto update = new NameUpdateDto { FamilyName = "NewFamilyName" };
			var updateResult = await client.UpdatePersonAsync(251, update);

			if ( updateResult switch { ProblemDetails p => !(p.Status == System.Net.HttpStatusCode.NotFound), _ => true } )
				Assert.Fail("updateResult was expected to be a ProblemDetails with status NotFound(404).");
		}

		private static PersonenVerwaltungClient CreateApiClient()
		{
			if ( _client == null )
				throw new InvalidOperationException();

			return _client;
		}


		static MsSqlContainer?
			_container;

		static TestWebApplicationFactory<Program>?
			_factory;

		static PersonenVerwaltungClient?
			_client;
	}

	public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
	{
		public TestWebApplicationFactory(string sqlConnectionString)
		{
			_connectionString = sqlConnectionString;
		}

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				ServiceDescriptor toRemove = services.Single(e => e.ServiceType == typeof(IDbContextOptionsConfiguration<PersonalVerwaltungContext>));

				services.Remove(toRemove);

#pragma warning disable EF1001 // Internal EF Core API usage.
				services.Add(
					new ServiceDescriptor(
						typeof(IDbContextOptionsConfiguration<PersonalVerwaltungContext>),
						p => new DbContextOptionsConfiguration<PersonalVerwaltungContext>((_, options) => options.UseSqlServer(_connectionString)),
						toRemove.Lifetime));
#pragma warning restore EF1001 // Internal EF Core API usage.
			});

			builder.UseEnvironment("Development");
		}


		readonly string
			_connectionString;
	}
}
