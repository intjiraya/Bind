namespace Bind.Domain.Players.ValueObjects;

public sealed class SteamId
{
    public string Value { get; }

    public SteamId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SteamId cannot be empty.", nameof(value));
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d{17}$"))
            throw new ArgumentException("SteamId must be a 17-digit numeric string.", nameof(value));

        Value = value;
    }

    public override bool Equals(object? obj) => obj is SteamId other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
    public static bool operator ==(SteamId a, SteamId b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(SteamId a, SteamId b) => !(a == b);
}