using Bunker.Domain.Core;
using Bunker.Domain.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bunker.Infrastructure;

internal sealed class ApplicationDbContext : DbContext
{
  public DbSet<Game> Games { get; set; } = null!;
  public DbSet<Member> Members { get; set; } = null!;
  public DbSet<MemberGameData> MembersGameData { get; set; } = null!;
  public DbSet<MemberGameInfo> MembersGameInfo { get; set; } = null!;
  public DbSet<MemberInfo> MembersInfo { get; set; } = null!;
  public DbSet<Disaster> Maps { get; set; } = null!;

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
    
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder
      .Entity<Disaster>()
      .HasData(
        Disaster.Create("Nuclear War"),
        Disaster.Create("Virus"));

    modelBuilder
      .Entity<MemberInfo>()
      .HasData(
        MemberInfo.Create(InfoType.Gender, "Male"),
        MemberInfo.Create(InfoType.Gender, "Female"),
        MemberInfo.Create(InfoType.Item, "Knife"),
        MemberInfo.Create(InfoType.Item, "Nothing"),
        MemberInfo.Create(InfoType.MentalHealth, "Healthy"),
        MemberInfo.Create(InfoType.MentalHealth, "Unhealthy"),
        MemberInfo.Create(InfoType.PhysicalHeath, "Healthy"),
        MemberInfo.Create(InfoType.PhysicalHeath, "Unhealthy"),
        MemberInfo.Create(InfoType.Talent, "None"),
        MemberInfo.Create(InfoType.Work, "None"));
  }
}