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

        public static Error InvalidDiscordId => Error.Validation(
            code: "Player.InvalidDiscordId",
            description: "The provided DiscordID format is incorrect.");

        public static Error InvalidIpAddress => Error.Validation(
            code: "Player.InvalidIpAddress",
            description: "The provided IP address format is incorrect.");

        public static Error InvalidPlayerId => Error.Validation(
            code: "Player.InvalidPlayerId",
            description: "The provided PlayerID format is incorrect.");

        public static Error InvalidSteamId => Error.Validation(
            code: "Player.InvalidSteamId",
            description: "The provided SteamID format is incorrect.");
    }

    public static class PlayerFilter
    {
        public static Error Empty => Error.Validation(
            code: "PlayerFilter.Empty",
            description: "At least one identifier must be provided.");
    }
}