namespace FCG.Users.Application.Security;

public interface ITokenSettings
{
    short ExpiracaoAccessTokenMinutos { get; }
    byte ExpiracaoRefreshTokenDias { get; }
    bool HabilitarSegurancaDeReusoRefreshToken { get; }
}
