using Bind.Contracts.Players.API;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

namespace Bind.Application.Players.Interfaces;

public interface IGetPlayerByDiscordIdUseCase
{
    Task<ErrorOr<PlayerResponse>> ExecuteAsync(DiscordId discordId, CancellationToken ct);
}