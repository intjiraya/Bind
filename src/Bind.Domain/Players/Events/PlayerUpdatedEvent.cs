namespace Bind.Domain.Players.Events;

public sealed class PlayerUpdatedEvent
{
    public Guid PlayerId { get; }
    public string DiscordId { get; }
    public DateTime UpdatedAt { get; }

    public PlayerUpdatedEvent(Guid playerId, string discordId, DateTime updatedAt)
    {
        PlayerId = playerId;
        DiscordId = discordId;
        UpdatedAt = updatedAt;
    }
}