using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;

namespace Yatt_Service.Auth.Keycloak
{
    public static class KeycloakInitializer
    {
        public static void AddKeycloak(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

            services.AddKeycloakWebApiAuthentication(configuration);
            services.AddAuthorization().AddKeycloakAuthorization(configuration);
        }
    }
}
