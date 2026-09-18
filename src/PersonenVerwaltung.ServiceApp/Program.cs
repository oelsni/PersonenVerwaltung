using Microsoft.EntityFrameworkCore;

using PersonenVerwaltung;
using PersonenVerwaltung.App.GetPerson;
using PersonenVerwaltung.App.ListPersons;
using PersonenVerwaltung.App.PersonServiceImpl;
using PersonenVerwaltung.App.UpdatePerson;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MSSql") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<PersonalVerwaltungContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IPersonService, EFCorePersonService>();
builder.AddServiceDefaults();

// Add services to the container.

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet(GetPersonEndpoint.Pattern, GetPersonEndpoint.HandleRequest);
app.MapGet(ListPersonsEndpoint.Pattern, ListPersonsEndpoint.HandleRequest);
app.MapPatch(UpdatePersonEndpoint.Pattern, UpdatePersonEndpoint.HandleRequest);

app.Run();
