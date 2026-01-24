using EGAR.Infrastructure.Data.MsSQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EGAR.Infrastructure.Data.MsSQL.Context;

public class AppDbContext : DbContext
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public virtual DbSet<OrderEntity> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //configuration.GetConnectionString(DbOptions.MsSqlConnection)
            optionsBuilder.UseSqlServer("Server=localhost,1405;Database=EGARDB;User=sa;Password=StrongP@ssw0rd!;TrustServerCertificate=true;MultipleActiveResultSets=true");
        }
    }
}