using FCG.Users.Domain.Entities;
using FCG.Users.Domain.Enums;
using FCG.Users.Domain.Repositories;
using FCG.Shared.Domain.Exceptions;

namespace FCG.Users.Application.UseCases;

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
