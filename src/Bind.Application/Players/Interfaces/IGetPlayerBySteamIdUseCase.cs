using Bind.Contracts.Players.API;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

namespace Bind.Application.Players.Interfaces;

public interface IGetPlayerBySteamIdUseCase
{
    Task<ErrorOr<PlayerResponse>> ExecuteAsync(SteamId steamId, CancellationToken ct);
}