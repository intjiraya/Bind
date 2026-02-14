using Bind.Domain.Players.Aggregates;
using Bind.Domain.Players.ValueObjects;

namespace Bind.Application.Players.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetByIdAsync(Guid id, CancellationToken ct);

    Task<Player?> GetBySteamIdAsync(SteamId steamId, CancellationToken ct);
    
    Task<bool> ExistsAsync(SteamId steamId, CancellationToken ct);

    Task AddAsync(Player player, CancellationToken ct);
}