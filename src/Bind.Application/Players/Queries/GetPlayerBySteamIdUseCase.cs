using Bind.Application.Players.Interfaces;
using Bind.Contracts.Players.API;
using Bind.Domain.Players;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

namespace Bind.Application.Players.Queries;

public class GetPlayerBySteamIdUseCase(IPlayerRepository repository) : IGetPlayerBySteamIdUseCase
{
    public async Task<ErrorOr<PlayerResponse>> ExecuteAsync(SteamId steamId, CancellationToken ct)
    {
        var player = await repository.GetBySteamIdAsync(steamId, ct);
        if (player is null)
            return Errors.Player.NotFound;

        return new PlayerResponse(player.Id, player.SteamId.Value, player.DiscordId?.Value);
    }
}