using Bind.Application.Players.Interfaces;
using Bind.Contracts.Players.API;
using Bind.Domain.Players;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

namespace Bind.Application.Players.Queries;

public class GetPlayerUseCase(IPlayerRepository repository) : IGetPlayerUseCase
{
    public async Task<ErrorOr<PlayerResponse>> ExecuteAsync(
        PlayerId? playerId,
        SteamId? steamId,
        DiscordId? discordId,
        CancellationToken ct)
    {
        var filter = new PlayerFilter(playerId, steamId, discordId);
        if (filter.IsEmpty)
            return Errors.PlayerFilter.Empty;

        var player = await repository.GetByFilterAsync(filter, ct);
        if (player is null)
            return Errors.Player.NotFound;

        return new PlayerResponse(player.Id.ToString(), player.SteamId.Value, player.DiscordId?.Value);
    }
}