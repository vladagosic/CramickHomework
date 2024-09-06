using Microsoft.Extensions.Configuration;

namespace CramickHomework.Application.Extensions
{
	public static class ConfigurationExtensions
	{
		public static bool RequireConfirmedEmail(this IConfiguration configuration)
			=> bool.Parse(configuration["Identity:SignIn:RequireConfirmedEmail"] ?? Constants.Identity.DefaultRequireConfirmedEmail.ToString());

		public static bool UseDeveloperExceptions(this IConfiguration configuration)
			=> bool.Parse(configuration["UseDeveloperExceptions"] ?? bool.FalseString);

		public static bool HideSystemExceptionMessages(this IConfiguration configuration)
			=> bool.Parse(configuration["HideSystemExceptionMessages"] ?? bool.TrueString);

		public static bool PerformanceLoggingEnabled(this IConfiguration configuration)
			=> bool.Parse(configuration["PerformanceLogging:Enabled"] ?? bool.FalseString);

		public static int LongRunngingTaskTresholdSeconds(this IConfiguration configuration)
			=> int.Parse(configuration["PerformanceLogging:LongRunngingTaskTresholdSeconds"] ?? Constants.Performance.LongRunngingTaskTresholdSeconds.ToString());

		public static string ClientApplicationBaseUrl(this IConfiguration configuration)
			=> configuration["JWT:ValidAudience"]!;

		public static string ServerApplicationBaseUrl(this IConfiguration configuration)
			=> configuration["JWT:ValidIssuer"]!;

		public static IConfiguration GetEnvironmentConfiguration()
		{
			var configurationBuilder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, true);

			return
				TryGetEnvironment(out var environment) ?
				configurationBuilder.AddJsonFile($"appsettings.{environment}.json", true).Build() :
				configurationBuilder.Build();
		}

		public static bool TryGetEnvironment(out string? environment)
		{
			var env = Environment.GetEnvironmentVariable(Constants.Environment.VariableName);
			environment = env;

			return
				Constants.Environment.Names.Any(
					x => x.Equals(env, StringComparison.InvariantCultureIgnoreCase));
		}

		public static T ReadSettingsSection<T>(this IConfiguration configuration, string sectionName)
		{
			var sectionSettings = configuration.GetSection(sectionName).Get<T>();
			return 
				sectionSettings is null ? 
				throw new ApplicationException($"Section is missing from configuration. Section Name: {sectionName}")
				: sectionSettings;
		}
	}
}
