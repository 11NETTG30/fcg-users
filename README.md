# fcg-users

Microsserviço de usuários do FIAP Cloud Games. Responsável por cadastro, autenticação JWT e autorização.

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL 18](https://www.postgresql.org/download/)
- Acesso ao GitHub Packages da organização [11NETTG30](https://github.com/11NETTG30)

## Configuração do NuGet (GitHub Packages)

Este projeto consome pacotes NuGet publicados no GitHub Packages. Como o GitHub Packages exige autenticação mesmo para pacotes públicos, é necessário configurar um **Personal Access Token (PAT)** na sua máquina antes de rodar o restore.

### 1. Criar o PAT no GitHub

1. Acesse **GitHub → Settings → Developer settings → Personal access tokens → Tokens (classic)**
2. Clique em **Generate new token (classic)**
3. Dê um nome (ex: `fcg-nuget-read`) e selecione o escopo **`read:packages`**
4. Clique em **Generate token** e copie o valor gerado

### 2. Configurar o PAT localmente

Execute o comando abaixo substituindo `SEU_USUARIO` e `SEU_TOKEN`:

```bash
dotnet nuget add source "https://nuget.pkg.github.com/11NETTG30/index.json" --name "github" --username "SEU_USUARIO" --password "SEU_TOKEN" --store-password-in-clear-text --configfile ~/.nuget/NuGet/NuGet.Config
```

> As credenciais são salvas no arquivo global da sua máquina (`~/.nuget/NuGet/NuGet.Config`) e **nunca entram no repositório**.

### 3. Restaurar dependências

```bash
dotnet restore FCG.Users.slnx
```

## Executar os testes

```bash
dotnet test FCG.Users.slnx
```
