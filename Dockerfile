# Imagem base de runtime usada na etapa final — sem SDK, menor e mais segura
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
USER app

# Etapa de build: restaura dependências (incluindo pacotes privados do GitHub Packages)
# e compila o projeto
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY nuget.config .
COPY src/FCG.Users.Domain/FCG.Users.Domain.csproj src/FCG.Users.Domain/
COPY src/FCG.Users.Application/FCG.Users.Application.csproj src/FCG.Users.Application/
COPY src/FCG.Users.Infrastructure/FCG.Users.Infrastructure.csproj src/FCG.Users.Infrastructure/
COPY src/FCG.Users.IoC/FCG.Users.IoC.csproj src/FCG.Users.IoC/
COPY src/FCG.Users.API/FCG.Users.API.csproj src/FCG.Users.API/

RUN --mount=type=secret,id=nuget_token \
    NUGET_AUTH_TOKEN=$(cat /run/secrets/nuget_token) \
    dotnet restore src/FCG.Users.API/FCG.Users.API.csproj

COPY . .

RUN dotnet build src/FCG.Users.API/FCG.Users.API.csproj \
    -c $BUILD_CONFIGURATION --no-restore -o /app/build

# Etapa de publicação: gera os artefatos otimizados para produção
FROM build AS publish
RUN dotnet publish src/FCG.Users.API/FCG.Users.API.csproj \
    -c $BUILD_CONFIGURATION --no-restore -o /app/publish

# Imagem final: copia apenas os artefatos publicados para a imagem base de runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.Users.API.dll"]
