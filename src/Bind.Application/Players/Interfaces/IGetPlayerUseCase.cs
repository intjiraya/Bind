using Bind.Contracts.Players.API;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

namespace Bind.Application.Players.Interfaces;

public interface IGetPlayerUseCase
{
    Task<ErrorOr<PlayerResponse>> ExecuteAsync(
        PlayerId? playerId,
        SteamId? steamId,
        DiscordId? discordId,
        CancellationToken ct
    );
}