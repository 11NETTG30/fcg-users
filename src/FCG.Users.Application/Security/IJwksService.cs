namespace FCG.Users.Application.Security;

public record ChavePublicaJwkDto(string Kty, string Use, string Kid, string Alg, string N, string E);

public record ChavesPublicasDto(IReadOnlyList<ChavePublicaJwkDto> Keys);

public interface IJwksService
{
    ChavesPublicasDto ObterChavesPublicas();
}
