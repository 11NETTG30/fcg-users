using FCG.Users.API.Configurations;
using FCG.Users.API.Middlewares;
using FCG.Users.Infrastructure.Configurations;
using FCG.Users.Infrastructure.Identidade.Configurations;
using FCG.Users.IoC;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddLoggingConfiguration();
builder.Services.AddControllersConfiguration();
builder.Services.AddDocumentation();
builder.Services.AddProblemDetailsConfiguration();
builder.Services.ConfigureModelStateInvalid();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDependencies();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
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

app.Run();
