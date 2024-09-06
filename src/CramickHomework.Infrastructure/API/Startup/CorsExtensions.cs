using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class CorsExtensions
	{
		public static IServiceCollection AppAddCors(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAngularDevClient",
					b =>
					{
						b
						.WithOrigins(configuration["JWT:ValidAudience"]!)
						.AllowAnyHeader()
						.AllowAnyMethod();
					});
			});

			return services;
		}
	}
}
