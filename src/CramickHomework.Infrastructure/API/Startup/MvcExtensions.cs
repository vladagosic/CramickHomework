using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CramickHomework.Infrastructure.API.Filters;
using CramickHomework.Infrastructure.Extensions;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class MvcExtensions
	{
		public static IServiceCollection AppAddControllers(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers(options =>
			{
				options.Filters.Add(new HandleExceptionsFilterAttribute(configuration));
			})
			.ConfigureApiBehaviorOptions(options =>
			{
				options.InvalidModelStateResponseFactory = 
					context =>
					context.HttpContext.CreateValidationProblemDetailsResponse(context.ModelState);
			});

			return services;
		}
	}
}
