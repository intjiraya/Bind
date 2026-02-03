using Bind.Domain.Players.Events;
using Bind.Domain.Players.ValueObjects;

namespace Bind.Domain.Players.Aggregates;

public class Player
{
    public Guid Id { get; private set; }
    public SteamId SteamId { get; private set; }
    public DiscordId? DiscordId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private readonly List<object> _domainEvents = new();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private Player()
    {
    }

    private Player(SteamId steamId)
    {
        Id = Guid.NewGuid();
        SteamId = steamId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        _domainEvents.Add(new PlayerCreatedEvent(Id, steamId.Value, CreatedAt));
    }

    public static Player Create(SteamId steamId) => new(steamId);

    public void UpdateDiscordId(DiscordId discordId)
    {
        if (DiscordId == discordId)
            return;

        DiscordId = discordId;
        UpdatedAt = DateTime.UtcNow;
        _domainEvents.Add(new PlayerUpdatedEvent(Id, discordId.Value, UpdatedAt));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}