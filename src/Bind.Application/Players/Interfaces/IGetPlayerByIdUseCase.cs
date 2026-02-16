using Bind.Contracts.Players.API;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

namespace Bind.Application.Players.Interfaces;

public interface IGetPlayerByIdUseCase
{
    Task<ErrorOr<PlayerResponse>> ExecuteAsync(PlayerId playerId, CancellationToken ct);
}