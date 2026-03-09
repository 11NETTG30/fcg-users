using FCG.Users.Domain.Entities;

namespace FCG.Users.Domain.Services;

public interface IRefreshTokenDomainService
{
    Task RevogarCadeiaDescendente(RefreshToken refreshToken, Guid refreshTokenId);
}
