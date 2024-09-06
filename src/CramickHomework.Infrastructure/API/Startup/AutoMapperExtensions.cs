using Microsoft.Extensions.DependencyInjection;
using CramickHomework.Application.Helpers;
using System.Reflection;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class AutoMapperExtensions
	{
		public static void AppAddAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
		{
			services.AddAutoMapper(AssemblyFinder.ApplicationAssembly);
		}
	}
}
