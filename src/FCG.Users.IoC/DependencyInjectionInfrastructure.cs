using FCG.Users.Application.Security;
using FCG.Shared.Application;
using FCG.Users.Domain.Repositories;
using FCG.Users.Domain.Security;
using FCG.Shared.Domain.Abstractions;
using FCG.Users.Infrastructure.Configurations;
using FCG.Users.Infrastructure.Persistence.Repositories;
using FCG.Users.Infrastructure.Security;
using FCG.Shared.Infrastructure;
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
