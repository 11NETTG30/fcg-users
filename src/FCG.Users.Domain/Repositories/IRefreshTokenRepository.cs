using FCG.Users.Domain.Entities;
using FCG.Shared.Domain.Abstractions;

namespace FCG.Users.Domain.Repositories;

public interface IRefreshTokenRepository: IRepository<RefreshToken>
{
    Task<RefreshToken?> ObterPorId(Guid id);
    Task<RefreshToken?> ObterPorToken(Guid refreshToken);
    Task Adicionar(RefreshToken refreshToken);
    Task<List<RefreshToken>> ListarNaoRevogadosPorUsuario(Guid usuarioId);
}
