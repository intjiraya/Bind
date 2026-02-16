namespace Bind.Contracts.Players.API;

public record PlayerResponse(string PlayerId, string SteamId, string? DiscordId);