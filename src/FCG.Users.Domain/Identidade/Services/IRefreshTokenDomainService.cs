using FCG.Users.Domain.Identidade.Entities;

namespace FCG.Users.Domain.Identidade.Services;

public interface IRefreshTokenDomainService
{
    Task RevogarCadeiaDescendente(RefreshToken refreshToken, Guid refreshTokenId);
}
