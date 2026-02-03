using Bind.Application.Players.Commands;
using Bind.Application.Players.Interfaces;
using Bind.Contracts.Players.API;
using Bind.Domain.Players.ValueObjects;

namespace Bind.Web.Endpoints;

public static class PlayerEndpoints
{
    public static void MapPlayerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/players");

        // Создание нового Player
        group.MapPost("/", async (CreatePlayerRequest request, ICreatePlayerUseCase useCase) =>
        {
            var steamId = new SteamId(request.SteamId);
            var player = await useCase.HandleAsync(steamId);
            return Results.Created($"/api/players/{player.Id}", new PlayerResponseDto(player));
        });
        

        // Получение Player по SteamId
        group.MapGet("/{steamId}", async (string steamId, IGetPlayerBySteamIdUseCase useCase) =>
        {
            var player = await useCase.HandleAsync(new SteamId(steamId));
            if (player is null) return Results.NotFound();
            return Results.Ok(new PlayerResponseDto(player));
        });

        // Обновление DiscordId
        group.MapPatch("/{playerId}/discord", async (Guid playerId, UpdateDiscordIdRequestDto request, IUpdatePlayerDiscordIdUseCase useCase) =>
        {
            var discordId = new DiscordId(request.DiscordId);
            var player = await useCase.HandleAsync(playerId, discordId);
            if (player is null) return Results.NotFound();
            return Results.Ok(new PlayerResponseDto(player));
        });
    }
}