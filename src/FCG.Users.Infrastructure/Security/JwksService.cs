using System.Security.Cryptography;
using FCG.Users.Application.Security;
using FCG.Users.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FCG.Users.Infrastructure.Security;

public sealed class JwksService : IJwksService
{
    private readonly ChavesPublicasDto _chavesPublicas;

    public JwksService(IOptions<JwtSettings> jwtSettings)
    {
        var rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(jwtSettings.Value.ChavePrivadaRsaBase64), out _);
        RsaSecurityKey chavePublica = new(rsa) { KeyId = JwtSettings.ChaveId };

        JsonWebKey jwk = JsonWebKeyConverter.ConvertFromRSASecurityKey(chavePublica);

        ChavePublicaJwkDto chaveDto = new(
            Kty: jwk.Kty,
            Use: "sig",
            Kid: JwtSettings.ChaveId,
            Alg: "RS256",
            N: jwk.N,
            E: jwk.E
        );

        _chavesPublicas = new ChavesPublicasDto([chaveDto]);
    }

    public ChavesPublicasDto ObterChavesPublicas() => _chavesPublicas;
}
