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
                player => Results.Created($"/api/players/{player.Id}", player),
                errors => errors.Problem()
            );
        });
    }
}