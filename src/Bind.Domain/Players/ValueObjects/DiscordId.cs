namespace Bind.Domain.Players.ValueObjects;

public sealed class DiscordId
{
    public string Value { get; }

    public DiscordId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("DiscordId cannot be empty.", nameof(value));
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d{17,20}$"))
            throw new ArgumentException("DiscordId must be a numeric string between 17 and 20 digits.", nameof(value));

        Value = value;
    }

    public override bool Equals(object? obj) => obj is DiscordId other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
    public static bool operator ==(DiscordId a, DiscordId b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(DiscordId a, DiscordId b) => !(a == b);
}