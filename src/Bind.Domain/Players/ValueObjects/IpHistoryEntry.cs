using ErrorOr;
using static System.Text.RegularExpressions.Regex;

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
        if (string.IsNullOrWhiteSpace(value) || !IsMatch(
                value,
                @"^\b(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(?:\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}\b$"))
            return Errors.Player.InvalidIpAddress;

        return new IpHistoryEntry(value, seenAt);
    }
}