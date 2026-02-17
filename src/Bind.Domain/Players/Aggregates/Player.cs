using Bind.Domain.Players.Events;
using Bind.Domain.Players.ValueObjects;
using ErrorOr;

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

    private Player(SteamId steamId, NicknameHistoryEntry nicknameEntry, IpHistoryEntry ipEntry)
    {
        var now = DateTime.UtcNow;

        Id = Guid.NewGuid();
        SteamId = steamId;
        CurrentName = nicknameEntry.Value;
        CurrentIpAddress = ipEntry.Value;
        CreatedAt = now;
        UpdatedAt = now;

        _nicknameHistory.Add(nicknameEntry);
        _ipHistory.Add(ipEntry);

        _domainEvents.Add(new PlayerCreatedEvent(Id, steamId.Value, now));
    }

    public static ErrorOr<Player> Create(SteamId steamId, string name, string ip)
    {
        var now = DateTime.UtcNow;

        var nicknameResult = NicknameHistoryEntry.Create(name, now);
        if (nicknameResult.IsError)
            return nicknameResult.Errors;

        var ipResult = IpHistoryEntry.Create(ip, now);
        if (ipResult.IsError)
            return ipResult.Errors;

        return new Player(steamId, nicknameResult.Value, ipResult.Value);
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName) || CurrentName == newName)
            return;

        var entryResult = NicknameHistoryEntry.Create(newName, DateTime.UtcNow);
        _nicknameHistory.Add(entryResult.Value);

        CurrentName = newName;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RegisterIpAddress(string ipAddress)
    {
        if (CurrentIpAddress == ipAddress)
            return;

        var ipResult = IpHistoryEntry.Create(ipAddress, DateTime.UtcNow);
        _ipHistory.Add(ipResult.Value);

        CurrentIpAddress = ipAddress;
        UpdatedAt = DateTime.UtcNow;
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