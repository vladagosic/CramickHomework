using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Infrastructure.Persistence;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class IdentityExtensions
	{
		public static IServiceCollection AppAddIndentity(this IServiceCollection services, IConfiguration configuration) 
		{
			services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services
				.Configure<IdentityOptions>(options =>
				{
					options.User.RequireUniqueEmail = true;
					options.SignIn.RequireConfirmedEmail = configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");

					options.Password.RequireNonAlphanumeric = configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
					options.Password.RequireLowercase = configuration.GetValue<bool>("Identity:Password:RequireLowercase");
					options.Password.RequireUppercase = configuration.GetValue<bool>("Identity:Password:RequireUppercase");
					options.Password.RequiredLength = configuration.GetValue<int>("Identity:Password:RequiredLength");

				});

			return services;
		}
	}
}
