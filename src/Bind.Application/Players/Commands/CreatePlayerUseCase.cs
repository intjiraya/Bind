using Bind.Application.Players.Interfaces;
using Bind.Contracts.Players.API;
using Bind.Domain.Interfaces;
using Bind.Domain.Players;
using Bind.Domain.Players.Aggregates;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

namespace Bind.Application.Players.Commands;

public class CreatePlayerUseCase(IPlayerRepository repository, IUnitOfWork unitOfWork) : ICreatePlayerUseCase
{
    public async Task<ErrorOr<PlayerResponse>> ExecuteAsync(
        SteamId steamId,
        string nickname,
        string ip,
        CancellationToken ct)
    {
        var isPlayerExists = await repository.ExistsAsync(steamId, ct);
        if (isPlayerExists)
            return Errors.Player.AlreadyExists;

        var player = Player.Create(steamId, nickname, ip);

        await repository.AddAsync(player, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new PlayerResponse(player.Id.ToString(), player.SteamId.Value, player.DiscordId?.Value);
    }
}