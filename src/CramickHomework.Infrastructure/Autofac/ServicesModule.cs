using Autofac;
using CramickHomework.Application.Interfaces;
using System.Reflection;
using Module = Autofac.Module;

namespace CramickHomework.Infrastructure.Autofac
{
	public class ServiceModules<TUserIdProvider> : Module
		where TUserIdProvider : ICurrentUserIdProvider
	{
		public Assembly[] Assemblies { private get; set; } = [];

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assemblies)
				.Where(type =>
					(type.Name.EndsWith("Service") ||
					 type.Name.EndsWith("Provider") ||
					 type.Name.EndsWith("Client")))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			builder
				.RegisterType<TUserIdProvider>()
				.As<ICurrentUserIdProvider>()
				.InstancePerLifetimeScope();
		}
	}
}
