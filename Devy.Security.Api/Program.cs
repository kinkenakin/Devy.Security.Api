using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Devy.Security.Api;
using Devy.Security.Api.Caching;
using Devy.Security.Api.InitializerExtensions;
using Devy.Security.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
AppSettings appSettings = new AppSettings();
appSettings.InitializeApplicationSettings(builder.Configuration);


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddSwaggerDocumentation("DevyApi", Path.Combine(AppContext.BaseDirectory, 
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<DevySecurityContext>(options =>
{
    options.UseSqlServer(appSettings.DevyConnectionString,
                         sqlServerOptionsAction: sqlOptions =>
                         {
                             sqlOptions.CommandTimeout(6000);
                             sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                         });
});

Assembly[] allAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                 .Where(a => a.FullName.StartsWith("Devy.", true, System.Globalization.CultureInfo.InvariantCulture)).ToArray();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(allAssemblies));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterInstance(AutoMapperConfig.Initialize().CreateMapper()).ExternallyOwned();
    containerBuilder.Register(c => new InMemCacheProvider()).As(typeof(ICacheProvider)).SingleInstance();
});


var app = builder.Build();

ILifetimeScope autofacRoot = app.Services.GetAutofacRoot();
CacheExtensions.SetCacheProvider(autofacRoot.Resolve<ICacheProvider>());

app.UseSwaggerDocumentation();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
