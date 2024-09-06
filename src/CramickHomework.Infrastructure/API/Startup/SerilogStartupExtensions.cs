using CramickHomework.Application;
using CramickHomework.Application.Extensions;
using Serilog;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class SerilogStartupExtensions
	{
		public static void AppConfigureSerilog()
		{
			LoggerConfiguration config = new LoggerConfiguration()
				.ReadFrom.Configuration(ConfigurationExtensions.GetEnvironmentConfiguration())
				.Enrich.FromLogContext()
				.Enrich.WithProperty("AppVersion", Constants.Application.Version);

			Log.Logger = config.CreateBootstrapLogger();

			// for enabling self diagnostics see https://github.com/serilog/serilog/wiki/Debugging-and-Diagnostics,
			// i.e. Serilog.Debugging.SelfLog.Enable(Console.Error);  //NOSONAR
		}
	}
}
