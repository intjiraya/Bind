using Bind.Application.Players.Interfaces;
using Bind.Application.Players.Queries;
using Bind.Domain.Players.Aggregates;
using Bind.Domain.Players.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bind.Infrastructure.Persistence.Ef;

public class PlayerRepository(PlayerDbContext context) : IPlayerRepository
{
    public async Task<Player?> GetByFilterAsync(PlayerFilter filter, CancellationToken ct)
    {
        var query = context.Players.AsQueryable();

        if (filter.PlayerId is not null)
            query = query.Where(p => p.Id == filter.PlayerId.Value);
        if (filter.SteamId is not null)
            query = query.Where(p => p.SteamId == filter.SteamId);
        if (filter.DiscordId is not null)
            query = query.Where(p => p.DiscordId == filter.DiscordId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ExistsAsync(SteamId steamId, CancellationToken ct) =>
        await context.Players.AnyAsync(p => p.SteamId == steamId, ct);

    public async Task AddAsync(Player player, CancellationToken ct) =>
        await context.Players.AddAsync(player, ct);
}