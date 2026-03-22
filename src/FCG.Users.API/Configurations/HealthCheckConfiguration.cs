using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FCG.Users.API.Configurations;

public static class HealthCheckConfiguration
{
    extension(IServiceCollection services)
    {
        public void AddHealthCheckConfiguration(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;

            services.AddHealthChecks()
                .AddNpgSql(connectionString, name: "postgresql", tags: ["ready"]);
        }
    }

    extension(WebApplication app)
    {
        public void MapHealthCheckEndpoints()
        {
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = EscreverRespostaSimples
            });

            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = EscreverRespostaDetalhada
            });
        }
    }

    private static Task EscreverRespostaSimples(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync($$"""{"status":"{{report.Status}}"}""");
    }

    private static async Task EscreverRespostaDetalhada(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";

        var entradas = report.Entries.Select(e => new
        {
            nome = e.Key,
            status = e.Value.Status.ToString(),
            descricao = e.Value.Description,
            duracao = e.Value.Duration.TotalMilliseconds
        });

        var resultado = new
        {
            status = report.Status.ToString(),
            duracaoTotal = report.TotalDuration.TotalMilliseconds,
            verificacoes = entradas
        };

        await context.Response.WriteAsJsonAsync(resultado);
    }
}
