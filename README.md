# fcg-users

Microsserviço de usuários do **FIAP Cloud Games (FCG)**.

Responsável por cadastro, autenticação JWT, autorização e exposição do endpoint JWKS. É a única autoridade de assinatura de tokens JWT no ecossistema FCG; os demais microsserviços apenas validam via `/.well-known/jwks.json`.

---

## 📚 Sobre o Projeto

Parte do **Tech Challenge da Pós-Graduação em Arquitetura de Sistemas .NET da FIAP**, **Turma 11NETT – Grupo 30**.

O FCG é uma plataforma de games educacionais. Este microsserviço foi extraído e refatorado a partir do monolito da Fase 1, adotando Clean Architecture, DDD e comunicação via eventos com RabbitMQ.

---

## 🛠️ Tecnologias Utilizadas

| Categoria | Tecnologia / Ferramenta |
|---|---|
| Plataforma | .NET 10 |
| Framework Web | ASP.NET Core 10 |
| Linguagem | C# 14 |
| ORM / Persistência | Entity Framework Core 10 + Migrations |
| Banco de Dados | PostgreSQL 18 |
| Documentação API | OpenAPI + Swagger + Scalar |
| Autenticação | JWT RS256 (RSA assimétrico) + RefreshToken rotativo |
| Hash de senha | Argon2id (19 MiB, 2 iterações, paralelismo 1) |
| Mensageria | RabbitMQ + MassTransit |
| Validação | FluentValidation |
| Erros HTTP | RFC 7807 ProblemDetails |
| Testes Unitários | xUnit + Stryker.NET (mutação) |
| Testes BDD | Reqnroll + NUnit + Moq |

---

## 🏛️ Arquitetura

Clean Architecture com separação em 5 projetos:

```
src/
├── FCG.Users.Domain/          ← entidades, value objects, interfaces de domínio
├── FCG.Users.Application/     ← use cases, DTOs, validadores, interfaces de serviço
├── FCG.Users.Infrastructure/  ← EF Core, repositórios, JWT, Argon2id, MassTransit
├── FCG.Users.IoC/             ← registro de dependências
└── FCG.Users.API/             ← controllers, configurações, Program.cs

tests/
├── FCG.Users.Tests/           ← testes unitários (xUnit + Stryker)
└── FCG.Users.BDDTests/        ← testes de comportamento (Reqnroll + NUnit)
```

> Consulte o repositório [fcg-shared](https://github.com/11NETTG30/fcg-shared) para mais detalhes sobre os pacotes compartilhados e instruções de configuração do NuGet (GitHub Packages).

---

## 🔐 Autenticação JWT RS256

Este serviço utiliza **criptografia assimétrica RSA (RS256)**:

- A **chave privada** fica exclusivamente neste serviço (em `appsettings.Development.json`, gitignored) e é usada para **assinar** os tokens.
- A **chave pública** é exposta em `/.well-known/jwks.json` e usada pelos demais microsserviços para **validar** tokens sem precisar se comunicar com este serviço.

### Endpoints JWKS / OIDC

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/.well-known/jwks.json` | Chave pública RSA no formato JWKS |
| `GET` | `/.well-known/openid-configuration` | Metadados OIDC (issuer + jwks_uri) |

---

## 🚀 Executar Localmente

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL 18](https://www.postgresql.org/download/)
- Acesso ao GitHub Packages da organização [11NETTG30](https://github.com/11NETTG30) (veja [fcg-shared](https://github.com/11NETTG30/fcg-shared))

### 1. Configurar banco de dados

Crie o banco `fcg_users` no PostgreSQL e ajuste a connection string em `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=fcg_users;Username=SEU_USUARIO;Password=SUA_SENHA"
}
```

### 2. Configurar JWT RSA

Gere um par de chaves RSA 2048 (PKCS#8, DER) em Base64 (Linux / macOS / WSL / Git Bash):

```bash
openssl genrsa 2048 2>/dev/null | openssl pkcs8 -topk8 -nocrypt -outform DER | base64 -w 0; echo
```

Adicione o valor gerado em `appsettings.Development.json`:

```json
"Jwt": {
  "ChavePrivadaRsaBase64": "BASE64_DA_CHAVE_PRIVADA_RSA"
}
```

### 3. Executar a API

As migrations são aplicadas automaticamente na inicialização.

```bash
dotnet run --project src/FCG.Users.API
```

Acesse a documentação interativa:
- **Swagger:** `http://localhost:5083/swagger`
- **Scalar:** `http://localhost:5083/scalar`

### Credenciais do administrador (seed)

```json
{
  "email": "admin@fcg.com.br",
  "senha": "Admin@123"
}
```

---

## 🧪 Testes

```bash
# Todos os testes
dotnet test FCG.Users.slnx

# Apenas unitários
dotnet test tests/FCG.Users.Tests

# Apenas BDD
dotnet test tests/FCG.Users.BDDTests

# Análise de mutação (Stryker)
dotnet stryker --project FCG.Users.Domain.csproj
```
