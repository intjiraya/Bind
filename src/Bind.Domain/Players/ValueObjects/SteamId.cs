using ErrorOr;

namespace Bind.Domain.Players.ValueObjects;

public sealed record SteamId
{
    public string Value { get; }

    private SteamId(string value)
    {
        Value = value;
    }

    public static ErrorOr<SteamId> Create(string value)
    {
        if (value is not { Length: 17 }
            || !value.StartsWith("7656119")
            || !ulong.TryParse(value, out _))
            return Errors.Player.InvalidSteamId;

        return new SteamId(value);
    }

    public override string ToString() => Value;
}