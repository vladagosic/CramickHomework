using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using CramickHomework.Application;
using CramickHomework.Infrastructure.API.Swagger;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class SwaggerExtensions
	{
		public static void AppUseSwagger(this IApplicationBuilder app)
		{
			app.UseOpenApi();
			app.UseSwaggerUi();
		}

		public static IServiceCollection AppAddSwagger(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddEndpointsApiExplorer();
			services.AddOpenApiDocument();

			services.AddSwaggerGen(x =>
			{
				x.SwaggerDoc("v1", new OpenApiInfo { Title = Constants.Application.Title, Version = Constants.Application.Version });
				x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization header using the Bearer scheme. <br/> 
                      Enter 'Bearer' [space] and then your token in the text input below. <br/>
                      Example: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
				});
				x.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});
				x.CustomSchemaIds(t => new CustomSwaggerSchemaNameGenerator().Generate(t));
			});

			return services;
		}

		private static IServiceCollection AddOpenApiDocument(this IServiceCollection services)
		{
			services
				.AddOpenApiDocument(document =>
				{
					document.DocumentName = Constants.Application.Version;
					document.Title = Constants.Application.Title;
					document.Description = Constants.Application.Description;
					document.Version = Constants.Application.Version;
					document.SchemaSettings.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator();
				});

			return services;
		}
	}
}
