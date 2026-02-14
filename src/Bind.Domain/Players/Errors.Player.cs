using ErrorOr;

namespace Bind.Domain.Players;

public static partial class Errors
{
    public static class Player
    {
        public static Error AlreadyExists => Error.Conflict(
            code: "Player.AlreadyExists",
            description: "Player with this SteamID is already registered.");

        public static Error NotFound => Error.NotFound(
            code: "Player.NotFound",
            description: "Player with the specified ID was not found.");

        public static Error InvalidSteamId => Error.Validation(
            code: "Player.InvalidSteamId",
            description: "The provided SteamID format is incorrect.");
    }
}