using EGAR.Infrastructure.Data.MsSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGAR.Infrastructure.Data.MsSQL.Context;

public class AppDbContext : DbContext
{    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public virtual DbSet<OrderEntity> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}