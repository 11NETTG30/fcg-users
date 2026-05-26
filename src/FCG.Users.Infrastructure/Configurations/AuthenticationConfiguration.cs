using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FCG.Users.Infrastructure.Configurations;

public static class AuthenticationConfiguration
{
    extension(IServiceCollection services)
    {
        public void AddJwtAuthentication(IConfiguration configuration)
        {
            IConfigurationSection jwtSettings = configuration.GetSection("Jwt");

            services.AddOptions<JwtSettings>()
                .Bind(jwtSettings)
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }
    }
}
