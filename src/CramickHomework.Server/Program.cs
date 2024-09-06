using Autofac;
using Autofac.Extensions.DependencyInjection;
using CramickHomework.Infrastructure.API.Startup;
using CramickHomework.Infrastructure.Autofac;
using CramickHomework.Infrastructure.Persistence;
using Serilog;

SerilogStartupExtensions.AppConfigureSerilog();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

builder.Host.ConfigureContainer<ContainerBuilder>(config =>
{
	config.AppRegisterModules(options =>
		{
			options.RegisterModule<EntityFrameworkModule<ApplicationDbContext>>();
		});
});

builder.Services.AppAddIndentity(builder.Configuration);
builder.Services.AppAddJwtBearerAuthentication(builder.Configuration);

builder.Services.AppAddMediatR(builder.Configuration);

builder.Services.AppAddAutoMapper();

builder.Services.AppAddControllers(builder.Configuration);

builder.Services.AppAddSwagger(builder.Configuration);

builder.Services.AppAddFluentValidation();

builder.Services.AppAddCors(builder.Configuration);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.AppUseSwagger();

app.AppConfigureFluentValidation();

app.UseCors("AllowAngularDevClient");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.LogApplicationStart(builder.Configuration);

app.Run();
