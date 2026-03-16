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

            string? chaveBase64 = jwtSettings[nameof(JwtSettings.ChavePrivadaRsaBase64)];
            var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(chaveBase64!), out _);
            RsaSecurityKey chavePublica = new(rsa) { KeyId = JwtSettings.ChaveId };

            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings[nameof(JwtSettings.Emissor)],

                ValidateAudience = true,
                ValidAudience = jwtSettings[nameof(JwtSettings.Audiencia)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = chavePublica,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            services.AddAuthorization();
        }
    }
}
