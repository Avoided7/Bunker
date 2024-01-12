using Bunker.Application.Extensions;
using Bunker.Infrastructure;
using Bunker.Infrastructure.Extensions;
using Bunker.WebApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddCors();

builder.Services
  .AddDbContextInMemory()
  .AddRepository()
  .AddUnitOfWork()
  .AddApplicationMediatR()
  .AddApplicationServices();

var app = builder.Build();

app.SeedDb();

app.UseCors(config =>
{
  config.AllowAnyHeader();
  config.AllowAnyMethod();
  config.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/game");

app.Run();
