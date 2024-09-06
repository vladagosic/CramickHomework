using Autofac;
using Microsoft.AspNetCore.Http;
using CramickHomework.Application.Helpers;
using CramickHomework.Infrastructure.Autofac;
using CramickHomework.Infrastructure.Providers;
using System.Security.Claims;
using System.Security.Principal;

namespace CramickHomework.Infrastructure.API.Startup
{
	public static class ContainerBuilderStartupExtensions
	{
		public static void AppRegisterModules(
			this ContainerBuilder builder,
			Action<ContainerBuilder> appSpecificConfiguration = default!)
		{
			builder.Register(GetPrincipal).As<IPrincipal>().InstancePerLifetimeScope();

			AppRegisterCommonModules(builder);

			if (appSpecificConfiguration != default)
				appSpecificConfiguration(builder);
		}

		private static void AppRegisterCommonModules(this ContainerBuilder builder)
		{
			builder.RegisterModule<ConfigurationModule>();
			builder.RegisterModule(
				new ServiceModules<HttpContextUserIdProvider>()
				{
					Assemblies = 
					[
						AssemblyFinder.ApplicationAssembly, 
						AssemblyFinder.InfrastructureAssembly
					]
				});
		}

		private static ClaimsPrincipal GetPrincipal(IComponentContext c)
		{
			var httpContextAccessor = c.Resolve<IHttpContextAccessor>();
			return httpContextAccessor.HttpContext?.User
				?? throw new InvalidOperationException("HttpContext is not available or User is not authenticated.");
		}
	}
}
