using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using CramickHomework.Application.Helpers;
using CramickHomework.Infrastructure.Extensions;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class FluentValidationStartupExtensions
	{
		public static IServiceCollection AppAddFluentValidation(this IServiceCollection services)
		{
			return services
				.AddValidatorsFromAssembly(AssemblyFinder.ApplicationAssembly, ServiceLifetime.Transient);
		}

		public static void AppConfigureFluentValidation(this IApplicationBuilder app)
		{
			ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
		}

		public static void AppAddFluentValidationApiBehaviorOptions(this ApiBehaviorOptions options)
		{
			options.InvalidModelStateResponseFactory = context =>
				context.HttpContext.CreateValidationProblemDetailsResponse(context.ModelState);
		}
	}
}
