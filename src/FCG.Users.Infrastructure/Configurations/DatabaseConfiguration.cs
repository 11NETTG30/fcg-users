using FCG.Users.Infrastructure.Persistence;
using FCG.Shared.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FCG.Users.Infrastructure.Configurations;

public static class DatabaseConfiguration
{
    extension(IServiceCollection services)
    {
        public void AddDatabase(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton<AuditoriaSaveChangesInterceptor>();
            services.AddDatabasePostgreSQL<IdentidadeDbContext>(connectionString!);
        }

        private void AddDatabasePostgreSQL<T>(string connectionString) where T : DbContext
        {
            services.AddDbContext<T>((serviceProvider, options) =>
                options
                    .UseNpgsql(connectionString, optionsPostgres =>
                    {
                        optionsPostgres.MigrationsHistoryTable("__ef_migrations_history");
                    })
                    .AddInterceptors(serviceProvider.GetRequiredService<AuditoriaSaveChangesInterceptor>())
            );
        }
    }

    extension(WebApplication app)
    {
        public async Task AplicarMigracoesAsync()
        {
            const int maxTentativas = 10;
            const int esperaSegundos = 5;

            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IdentidadeDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IdentidadeDbContext>>();

            for (int tentativa = 1; tentativa <= maxTentativas; tentativa++)
            {
                try
                {
                    logger.LogInformation("Aplicando migrations (tentativa {Tentativa}/{Max})...", tentativa, maxTentativas);
                    await db.Database.MigrateAsync();
                    logger.LogInformation("Migrations aplicadas com sucesso.");
                    return;
                }
                catch (Exception ex) when (tentativa < maxTentativas)
                {
                    logger.LogWarning(ex, "Banco indisponível. Aguardando {Segundos}s antes de tentar novamente...", esperaSegundos);
                    await Task.Delay(TimeSpan.FromSeconds(esperaSegundos));
                }
            }
        }
    }
}
