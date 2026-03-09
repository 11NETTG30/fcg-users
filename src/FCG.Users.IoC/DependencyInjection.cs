using Microsoft.Extensions.DependencyInjection;

namespace FCG.Users.IoC;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddDependencies()
        {
            services.AddDomain();
            services.AddApplication();
            services.AddInfrastructure();
        }
    }
}
