using FCG.Users.Application.Identidade.Security;
using FCG.Users.Application.Shared;
using FCG.Users.Domain.Identidade.Repositories;
using FCG.Users.Domain.Identidade.Security;
using FCG.Users.Domain.Shared.Abstractions;
using FCG.Users.Infrastructure.Identidade.Configurations;
using FCG.Users.Infrastructure.Identidade.Persistence.Repositories;
using FCG.Users.Infrastructure.Identidade.Security;
using FCG.Users.Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FCG.Users.IoC;

public static class DependencyInjectionInfrastructure
{
    extension(IServiceCollection services)
    {
        internal void AddInfrastructure()
        {
            services.AddRepositories();

            services.AddSingleton(typeof(IDomainLogger<>), typeof(DomainLogger<>));
            services.AddScoped<IInformacoesUsuarioLogado, InformacoesUsuarioLogado>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<ISenhaHasher, Argon2IdSenhaHasher>();

            services.AddSingleton<ITokenSettings>(provider =>
            {
                JwtSettings jwtSettings = provider.GetRequiredService<IOptions<JwtSettings>>().Value;
                return jwtSettings;
            });
        }

        private void AddRepositories()
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
