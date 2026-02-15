using ErrorOr;
using static System.Text.RegularExpressions.Regex;

namespace Bind.Domain.Players.ValueObjects;

public sealed class DiscordId
{
    public string Value { get; }

    private DiscordId(string value)
    {
        Value = value;
    }

    public static ErrorOr<DiscordId> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsMatch(value, @"^\d{17,20}$"))
            return Errors.Player.InvalidDiscordId;

        return new DiscordId(value);
    }
}