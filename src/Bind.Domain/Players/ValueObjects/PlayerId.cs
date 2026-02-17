using ErrorOr;

namespace Bind.Domain.Players.ValueObjects;

public sealed record PlayerId
{
    public Guid Value { get; }

    private PlayerId(Guid value)
    {
        Value = value;
    }

    public static ErrorOr<PlayerId> Create(string value)
    {
        if (!Guid.TryParse(value, out var guid))
            return Errors.Player.InvalidPlayerId;

        return new PlayerId(guid);
    }

    public static PlayerId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}