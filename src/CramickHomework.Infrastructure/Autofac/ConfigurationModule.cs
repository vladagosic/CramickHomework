using Autofac;
using Microsoft.Extensions.Configuration;
using CramickHomework.Application.Extensions;
using CramickHomework.Infrastructure.Persistence.Options;

namespace CramickHomework.Infrastructure.Autofac
{
    public class ConfigurationModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(c => c.Resolve<IConfiguration>().ReadSettingsSection<DbContextSettings>("DbContext"))
				.AsSelf()
				.SingleInstance();
		}
	}
}
