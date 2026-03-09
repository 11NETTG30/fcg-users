using FCG.Users.Domain.Identidade.Entities;
using FCG.Users.Domain.Identidade.Enums;
using FCG.Users.Domain.Identidade.Repositories;
using FCG.Users.Domain.Shared.Exceptions;

namespace FCG.Users.Application.Identidade.UseCases;

public sealed class InativarUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public InativarUsuarioUseCase
    (
        IUsuarioRepository usuarioRepository,
        IRefreshTokenRepository refreshTokenRepository
    )
    {
        _usuarioRepository = usuarioRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task Executar(Guid usuarioId)
    {
        Usuario usuario = await _usuarioRepository.ObterPorIdTracking(usuarioId)
            ?? throw new ValidationException("Usuário não existe");

        usuario.SetAtivo(false);

        List<RefreshToken> refreshTokens = await _refreshTokenRepository
            .ListarNaoRevogadosPorUsuario(usuario.Id);

        refreshTokens.ForEach(refreshToken =>
        {
            refreshToken.Revogar(MotivoRevogacaoRefreshToken.InativacaoUsuario);
        });

        await _usuarioRepository.UnitOfWork.Commit();
    }
}
