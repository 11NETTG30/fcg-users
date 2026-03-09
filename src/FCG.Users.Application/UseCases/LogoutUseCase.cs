using FCG.Users.Application.DTOs;
using FCG.Users.Domain.Entities;
using FCG.Users.Domain.Enums;
using FCG.Users.Domain.Repositories;

namespace FCG.Users.Application.UseCases;

public sealed class LogoutUseCase
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LogoutUseCase
    (
        IRefreshTokenRepository refreshTokenRepository
    )
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task Executar(LogoutRequest request)
    {
        RefreshToken? token = await _refreshTokenRepository.ObterPorToken(request.RefreshToken);

        if (token is null)
            return;

        token.Revogar(MotivoRevogacaoRefreshToken.Logout);

        await _refreshTokenRepository.UnitOfWork.Commit();
    }
}
