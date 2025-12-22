namespace Yatt_Service.Auth.Keycloak
{
    public class KeycloakOptions
    {
        public string Realm { get; set; } = string.Empty;
        public string AuthServerUrl { get; set; } = string.Empty;
        public string Resource { get; set; } = string.Empty;
        public string SwaggerResource { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public bool VerifyTokenAudience { get; set; } = true;
        public bool BearerOnly { get; set; } = false;
        public string SslRequired { get; set; } = "external";
    }
}
