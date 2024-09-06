using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CramickHomework.Application.Extensions;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class LoggingExtensions
	{
		public static void LogApplicationStart(this WebApplication app, IConfiguration configuration)
		{
			app.Logger.LogInformation($"CramickHomework Server application started! Swagger available at {configuration.ServerApplicationBaseUrl()}/swagger and client at {configuration.ClientApplicationBaseUrl()}");
		}
	}
}
