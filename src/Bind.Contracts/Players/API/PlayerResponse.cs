namespace Bind.Contracts.Players.API;

public record PlayerResponse(Guid Id, string SteamId, string? DiscordId);