using ErrorOr;

namespace Bind.Domain.Players.ValueObjects;

public sealed record NicknameHistoryEntry
{
    public string Value { get; }

    public DateTime ChangedAt { get; }

    private NicknameHistoryEntry(string value, DateTime changedAt)
    {
        Value = value;
        ChangedAt = changedAt;
    }

    public static ErrorOr<NicknameHistoryEntry> Create(string value, DateTime changedAt)
    {
        return new NicknameHistoryEntry(value, changedAt);
    }
}