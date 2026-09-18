using Microsoft.Extensions.DependencyInjection;

using PersonenVerwaltung.ServiceClient;

namespace PersonenVerwaltung.FormsClient
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ServiceCollection services = new ServiceCollection();

			services.AddSingleton<MainForm>();
			services.AddHttpClient<PersonenVerwaltungClient>((sp, cl) =>
			{
				cl.BaseAddress = new Uri("https://localhost:7083/api/personal-verwaltung/v1/", UriKind.Absolute);
			});

			var provider = services.BuildServiceProvider();

			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(provider.GetRequiredService<MainForm>());
		}
	}
}