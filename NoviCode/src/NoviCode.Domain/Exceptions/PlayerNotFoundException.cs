namespace NoviCode.Exceptions
{
    public sealed class PlayerNotFoundException : PlayerExceptions
    {
        public override string ErrorCode =>  "PLAYER_NOT_FOUND";

        public PlayerNotFoundException() : base("Player not found."){ }
    }
}
