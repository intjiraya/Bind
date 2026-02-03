using Bind.Contracts.Players.API;

namespace Bind.Application.Players.Interfaces;

public interface ICreatePlayerUseCase
{
    Task<PlayerResponse> ExecuteAsync(CreatePlayerRequest request, CancellationToken ct);
}