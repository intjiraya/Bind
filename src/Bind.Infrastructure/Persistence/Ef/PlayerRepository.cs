using Bind.Application.Players.Interfaces;
using Bind.Domain.Players.Aggregates;
using Bind.Domain.Players.ValueObjects;

namespace Bind.Infrastructure.Persistence.Ef;

public class PlayerRepository(PlayerDbContext context) : IPlayerRepository
{
    public async Task<Player?> GetByIdAsync(Guid id, CancellationToken ct)
        => await context.Players.FindAsync([id], ct);

    public async Task<Player?> GetBySteamIdAsync(SteamId steamId, CancellationToken ct) =>
        await context.Players.FindAsync([steamId], ct);

    public async Task AddAsync(Player player, CancellationToken ct)
        => await context.Players.AddAsync(player, ct);
}