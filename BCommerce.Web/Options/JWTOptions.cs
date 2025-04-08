namespace BCommerce.Web.Options
{
    public class JWTOptions
    {
        public const string JWT = "JWT";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SigningKey { get; set; }

    }
}
