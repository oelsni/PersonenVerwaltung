var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PersonenVerwaltung_ServiceApp>("personen-verwaltung-service");
builder.AddProject<Projects.PersonenVerwaltung_FormsClient>("personen-verwaltung-formclient");

builder.Build().Run();
