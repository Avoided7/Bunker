using Bunker.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bunker.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddDbContextInMemory(this IServiceCollection services)
  {
    services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemory"));

    return services;
  }

  public static IServiceCollection AddRepository(this IServiceCollection services)
  {
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    return services;
  }

  public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
  {
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    return services;
  }
}