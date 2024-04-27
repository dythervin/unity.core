namespace Dythervin
{
    public static class Regexes
    {
        public const string AnyIncludingTypeParameters =
            "[^<>]*(?:(?:(?<Open><)[^<>]*)+(?:(?<Close-Open>>)[^<>]*)+[^<>]*)*";
    }
}