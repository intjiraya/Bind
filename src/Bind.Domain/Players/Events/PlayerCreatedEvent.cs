namespace Bind.Domain.Players.Events;

public sealed class PlayerCreatedEvent
{
    public Guid PlayerId { get; }
    public string SteamId { get; }
    public DateTime CreatedAt { get; }

    public PlayerCreatedEvent(Guid playerId, string steamId, DateTime createdAt)
    {
        PlayerId = playerId;
        SteamId = steamId;
        CreatedAt = createdAt;
    }
}