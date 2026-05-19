namespace RWAN10T.Api.Auth.Config
{
    public class TokenConfiguration
    {
        public string Audience { get; set; } = String.Empty;
        public string Issuer { get; set; } = String.Empty;
        public string Secret { get; set; } = String.Empty;
        public int Minutes { get; set; } = 0;
        public int DaysToExpiry { get; set; } = 0;

    }
}
