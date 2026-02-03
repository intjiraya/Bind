using Bind.Domain.Interfaces;
using Bind.Domain.Players.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Bind.Infrastructure.Persistence.Ef;

public class PlayerDbContext(DbContextOptions<PlayerDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Player> Players => Set<Player>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerDbContext).Assembly);
    }
}