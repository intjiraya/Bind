using ErrorOr;

namespace Bind.Domain.Players.ValueObjects;

public sealed record DiscordId
{
    public string Value { get; }

    private DiscordId(string value)
    {
        Value = value;
    }

    public static ErrorOr<DiscordId> Create(string value)
    {
        if (value is not { Length: >= 17 and <= 20 } || !ulong.TryParse(value, out _))
            return Errors.Player.InvalidDiscordId;

        return new DiscordId(value);
    }

    public override string ToString() => Value;
}