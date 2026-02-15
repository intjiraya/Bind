using ErrorOr;
using static System.Text.RegularExpressions.Regex;

namespace Bind.Domain.Players.ValueObjects;

public sealed class SteamId
{
    public string Value { get; }

    private SteamId(string value)
    {
        Value = value;
    }

    public static ErrorOr<SteamId> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsMatch(value, @"^7656119(\d{10})$"))
            return Errors.Player.InvalidSteamId;

        return new SteamId(value);
    }
}