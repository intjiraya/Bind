namespace Bind.Contracts.Players.API;

public record GetPlayerRequest(string? PlayerId, string? DiscordId, string? SteamId);