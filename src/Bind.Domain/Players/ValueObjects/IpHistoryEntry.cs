using System.Net;
using ErrorOr;

namespace Bind.Domain.Players.ValueObjects;

public sealed record IpHistoryEntry
{
    public string Value { get; }

    public DateTime SeenAt { get; }

    private IpHistoryEntry(string value, DateTime seenAt)
    {
        Value = value;
        SeenAt = seenAt;
    }

    public static ErrorOr<IpHistoryEntry> Create(string value, DateTime seenAt)
    {
        if (!IPAddress.TryParse(value, out _))
            return Errors.Player.InvalidIpAddress;

        return new IpHistoryEntry(value, seenAt);
    }

    public override string ToString() => $"{Value} @ {SeenAt:u}";
}