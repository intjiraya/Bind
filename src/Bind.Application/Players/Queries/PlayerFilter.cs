using Bind.Domain.Players.ValueObjects;

namespace Bind.Application.Players.Queries;

public record PlayerFilter(
    PlayerId? PlayerId = null,
    SteamId? SteamId = null,
    DiscordId? DiscordId = null)
{
    public bool IsEmpty => PlayerId is null && SteamId is null && DiscordId is null;
}