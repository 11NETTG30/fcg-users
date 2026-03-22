using FCG.Shared.Infrastructure.Configurations;
using FCG.Shared.Infrastructure.Middlewares;
using FCG.Users.API.Configurations;
using FCG.Users.Infrastructure.Configurations;
using FCG.Users.IoC;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddLoggingConfiguration();
builder.AddObservabilidade();
builder.Services.AddControllersConfiguration();
builder.Services.AddDocumentation();
builder.Services.AddProblemDetailsConfiguration();
builder.Services.ConfigureModelStateInvalid();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddHealthCheckConfiguration(builder.Configuration);
builder.Services.AddDependencies(builder.Configuration);

WebApplication app = builder.Build();

await app.AplicarMigracoesAsync();

if (app.Configuration.GetValue<bool>("Documentacao:Habilitada"))
{
    app.UseDocumentation();
}

app.UseGlobalExceptionMiddleware();
app.UseUnauthorizedAccessExceptionMiddleware();
app.UseDomainExceptionMiddleware();
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthCheckEndpoints();

app.Run();
