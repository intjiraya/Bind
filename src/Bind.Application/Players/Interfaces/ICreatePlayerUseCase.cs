using Bind.Contracts.Players.API;
using ErrorOr;

namespace Bind.Application.Players.Interfaces;

public interface ICreatePlayerUseCase
{
    Task<ErrorOr<PlayerResponse>> ExecuteAsync(CreatePlayerRequest request, CancellationToken ct);
}