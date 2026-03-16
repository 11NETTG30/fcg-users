using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Users.IoC;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddDependencies(IConfiguration configuration)
        {
            services.AddDomain();
            services.AddApplication();
            services.AddInfrastructure(configuration);
        }
    }
}
