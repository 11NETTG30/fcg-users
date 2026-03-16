using FCG.Shared.Application;
using FCG.Shared.Domain.Abstractions;
using FCG.Shared.Infrastructure;
using FCG.Users.Application.Abstractions.Messaging;
using FCG.Users.Application.Security;
using FCG.Users.Domain.Repositories;
using FCG.Users.Domain.Security;
using FCG.Users.Infrastructure.Configurations;
using FCG.Users.Infrastructure.Messaging;
using FCG.Users.Infrastructure.Persistence.Repositories;
using FCG.Users.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FCG.Users.IoC;

public static class DependencyInjectionInfrastructure
{
    extension(IServiceCollection services)
    {
        internal void AddInfrastructure(IConfiguration configuration)
        {
            services.AddRepositories();
            services.ConfigureMessaging(configuration);


            services.AddScoped<IInformacoesUsuarioLogado, InformacoesUsuarioLogado>();
            services.AddScoped<IEventPublisher, EventPublisherMassTransit>();

            services.AddSingleton(typeof(IDomainLogger<>), typeof(DomainLogger<>));
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
