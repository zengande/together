namespace Together.Core
{
    public class GlobalSettings
    {
        public const string IdentityEndpoint = "http://localhost:5006";

        public static string ClientId { get; internal set; }
        public static string ClientSecret { get; internal set; }
        public static string Scope { get; internal set; }
    }
}
