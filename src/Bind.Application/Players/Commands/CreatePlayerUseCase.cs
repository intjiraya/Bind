using Bind.Application.Players.Interfaces;
using Bind.Contracts.Players.API;
using Bind.Domain.Interfaces;
using Bind.Domain.Players.Aggregates;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

namespace Bind.Application.Players.Commands;

public class CreatePlayerUseCase(IPlayerRepository repository, IUnitOfWork unitOfWork) : ICreatePlayerUseCase
{
    public async Task<ErrorOr<PlayerResponse>> ExecuteAsync(CreatePlayerRequest request, CancellationToken ct)
    {
        var steamId = new SteamId(request.SteamId);

        var isPlayerExists = await repository.ExistsAsync(steamId, ct);
        if (isPlayerExists)
        {
            return Error.Conflict(
                code: "Player.AlreadyExists",
                description: "Player with this SteamID is already registered.");
        }

        var player = Player.Create(steamId);

        await repository.AddAsync(player, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new PlayerResponse(player.Id, player.SteamId.Value, player.DiscordId?.Value);
    }
}