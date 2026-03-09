using FCG.Users.Domain.Entities;
using FCG.Users.Infrastructure.Persistence.Configurations;
using FCG.Shared.Infrastructure.Persistence.UoW;
using Microsoft.EntityFrameworkCore;

namespace FCG.Users.Infrastructure.Persistence;

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
