using Bind.Application.Players.Interfaces;
using Bind.Contracts.Players.API;
using Bind.Domain.Players.ValueObjects;
using Bind.Web.Common.Http;

namespace Bind.Web.Endpoints;

public static class PlayerEndpoints
{
    public static void MapPlayerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/players");

        group.MapPost("/", async (CreatePlayerRequest request, ICreatePlayerUseCase useCase, CancellationToken ct) =>
        {
            var steamIdResult = SteamId.Create(request.SteamId);
            if (steamIdResult.IsError)
                return steamIdResult.Errors.Problem();

            var result = await useCase.ExecuteAsync(steamIdResult.Value, ct);

            return result.Match(
                player => Results.Created($"/api/players/{player.PlayerId}", player),
                errors => errors.Problem()
            );
        });

        group.MapGet("/", async (
            [AsParameters] GetPlayerRequest request,
            IGetPlayerUseCase useCase,
            CancellationToken ct) =>
        {
            var playerId = !string.IsNullOrWhiteSpace(request.PlayerId)
                ? PlayerId.Create(request.PlayerId).Value
                : null;

            var steamId = !string.IsNullOrWhiteSpace(request.SteamId)
                ? SteamId.Create(request.SteamId).Value
                : null;

            var discordId = !string.IsNullOrWhiteSpace(request.DiscordId)
                ? DiscordId.Create(request.DiscordId).Value
                : null;

            var result = await useCase.ExecuteAsync(playerId, steamId, discordId, ct);

            return result.Match(
                player => Results.Ok(player),
                errors => errors.Problem()
            );
        });

        group.MapGet("/{playerId}", async (string playerId, IGetPlayerByIdUseCase useCase, CancellationToken ct) =>
        {
            var playerIdResult = PlayerId.Create(playerId);
            if (playerIdResult.IsError)
                return playerIdResult.Errors.Problem();

            var result = await useCase.ExecuteAsync(playerIdResult.Value, ct);

            return result.Match(
                player => Results.Ok(player),
                errors => errors.Problem()
            );
        });
    }
}