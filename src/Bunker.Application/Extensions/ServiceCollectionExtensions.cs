using Bunker.Application.Disasters.Services;
using Bunker.Application.MembersGameData.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bunker.Application.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddApplicationMediatR(this IServiceCollection services)
  {
    services.AddMediatR(config =>
    {
      config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
    });

    return services;
  }

  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IDisasterService, DisasterService>();
    services.AddScoped<IMemberGameDataService, MemberGameDataService>();

    return services;
  }
}
