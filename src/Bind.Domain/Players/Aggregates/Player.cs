using Bind.Domain.Players.Events;
using Bind.Domain.Players.ValueObjects;

namespace Bind.Domain.Players.Aggregates;

public class Player
{
    public Guid Id { get; private set; }

    public SteamId SteamId { get; private set; }

    public DiscordId? DiscordId { get; private set; }

    public string CurrentName { get; private set; }

    public string CurrentIpAddress { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    private readonly List<object> _domainEvents = new();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private readonly List<NicknameHistoryEntry> _nicknameHistory = new();
    public IReadOnlyCollection<NicknameHistoryEntry> NicknameHistory => _nicknameHistory.AsReadOnly();

    private readonly List<IpHistoryEntry> _ipHistory = new();
    public IReadOnlyCollection<IpHistoryEntry> IpHistory => _ipHistory.AsReadOnly();

    private Player(SteamId steamId, string initialName, string initialIp)
    {
        var now = DateTime.UtcNow;

        Id = Guid.NewGuid();
        SteamId = steamId;
        CurrentName = initialName;
        CurrentIpAddress = initialIp;
        CreatedAt = now;
        UpdatedAt = now;

        _nicknameHistory.Add(NicknameHistoryEntry.Create(initialName, now));
        _ipHistory.Add(IpHistoryEntry.Create(initialIp, now));

        _domainEvents.Add(new PlayerCreatedEvent(Id, steamId.Value, now));
    }

    public static Player Create(SteamId steamId, string name, string ip) => new(steamId, name, ip);

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName) || CurrentName == newName)
            return;

        CurrentName = newName;
        UpdatedAt = DateTime.UtcNow;

        _nicknameHistory.Add(new NicknameHistoryEntry(newName, UpdatedAt));
    }

    public void RegisterIpAddress(string ipAddress)
    {
        if (CurrentIpAddress == ipAddress)
            return;

        CurrentIpAddress = ipAddress;
        UpdatedAt = DateTime.UtcNow;

        _ipHistory.Add(new IpHistoryEntry(ipAddress, UpdatedAt));
    }

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