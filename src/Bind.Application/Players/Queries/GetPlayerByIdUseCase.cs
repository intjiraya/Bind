using Bind.Application.Players.Interfaces;
using Bind.Contracts.Players.API;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

namespace Bind.Application.Players.Queries;

public class GetPlayerByIdUseCase : IGetPlayerByIdUseCase
{
    public Task<ErrorOr<PlayerResponse>> ExecuteAsync(PlayerId playerId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}