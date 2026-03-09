using FCG.Users.Domain.Identidade.Entities;
using FCG.Users.Infrastructure.Identidade.Persistence.Configurations;
using FCG.Users.Infrastructure.Shared.Persistence.UoW;
using Microsoft.EntityFrameworkCore;

namespace FCG.Users.Infrastructure.Identidade.Persistence;

public sealed class IdentidadeDbContext : DbContextUoW
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public IdentidadeDbContext(DbContextOptions<IdentidadeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IdentidadeDbContext).Assembly,
            type => type.Namespace == typeof(UsuarioConfiguration).Namespace
        );
    }

}
