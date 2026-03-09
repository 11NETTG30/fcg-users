using FCG.Users.Domain.Identidade.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Users.IoC;

public static class DependencyInjectionDomain
{
    extension(IServiceCollection services)
    {
        internal void AddDomain()
        {
            services.AddScoped<IRefreshTokenDomainService, RefreshTokenDomainService>();
        }
    }
}
