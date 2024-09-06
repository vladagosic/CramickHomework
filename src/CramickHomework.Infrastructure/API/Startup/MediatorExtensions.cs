using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CramickHomework.Application.Extensions;
using CramickHomework.Application.Helpers;
using CramickHomework.Infrastructure.Mediator;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class MediatRStartupExtensions
	{
		public static IServiceCollection AppAddMediatR(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			if (configuration.PerformanceLoggingEnabled())
				services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceLoggingBehavior<,>));

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyFinder.ApplicationAssembly));

			return services;
		}
	}
}
