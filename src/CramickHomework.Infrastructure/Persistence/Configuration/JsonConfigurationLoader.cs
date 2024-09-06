using Microsoft.Extensions.Configuration;
using ConfigurationExtensions = CramickHomework.Application.Extensions.ConfigurationExtensions;

namespace CramickHomework.Infrastructure.Persistence.Configuration
{
	internal static class JsonConfigurationLoader
	{
		internal static IConfiguration GetConfiguration()
		{
			var appsettingsPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\CramickHomework.Server"));

			var builder = new ConfigurationBuilder()
				.SetBasePath(appsettingsPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			return
				ConfigurationExtensions.TryGetEnvironment(out var environment) ?
				builder.AddJsonFile($"appsettings.{environment}.json", true).Build() :
				builder.Build();
		}
	}
}
