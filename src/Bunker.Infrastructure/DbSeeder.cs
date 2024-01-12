using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bunker.Infrastructure;

public static class DbSeeder
{
  public static void SeedDb(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.EnsureCreated();
  }
}