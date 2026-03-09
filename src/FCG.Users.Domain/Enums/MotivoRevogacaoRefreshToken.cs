namespace FCG.Users.Domain.Enums;

public enum MotivoRevogacaoRefreshToken : byte
{
    Substituicao = 1,
    Logout = 2,
    TokenAscendenteComprometido = 3,
    InativacaoUsuario = 4
}
