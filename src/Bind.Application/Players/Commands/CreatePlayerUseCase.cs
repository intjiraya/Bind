using Bind.Application.Players.Interfaces;
using Bind.Contracts.Players.API;
using Bind.Domain.Interfaces;
using Bind.Domain.Players.Aggregates;
using Bind.Domain.Players.ValueObjects;

namespace Bind.Application.Players.Commands;

public class CreatePlayerUseCase(IPlayerRepository repository, IUnitOfWork unitOfWork) : ICreatePlayerUseCase
{
    public async Task<PlayerResponse> ExecuteAsync(CreatePlayerRequest request, CancellationToken ct)
    {
        var player = Player.Create(new SteamId(request.SteamId));

        await repository.AddAsync(player, ct);

        await unitOfWork.SaveChangesAsync(ct);

        return new PlayerResponse(player.Id, player.SteamId.Value, player.DiscordId?.Value);
    }
}