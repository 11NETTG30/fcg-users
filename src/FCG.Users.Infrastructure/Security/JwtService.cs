using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FCG.Users.Application.Security;
using FCG.Users.Domain.Entities;
using FCG.Users.Domain.Enums;
using FCG.Users.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FCG.Users.Infrastructure.Security;

public sealed class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly RsaSecurityKey _chavePrivada;

    public JwtService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;

        var rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(_jwtSettings.ChavePrivadaRsaBase64), out _);
        _chavePrivada = new RsaSecurityKey(rsa) { KeyId = JwtSettings.ChaveId };
    }

    public string GerarAccessToken(Usuario usuario)
    {
        List<Claim> claims = ObterClaims(usuario);

        SigningCredentials credentials = new(_chavePrivada, SecurityAlgorithms.RsaSha256);

        SecurityTokenDescriptor securityTokenDescriptor = new()
        {
            Issuer = _jwtSettings.Emissor,
            Audience = _jwtSettings.Audiencia,
            Subject = new ClaimsIdentity(claims),
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiracaoAccessTokenMinutos),
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken? token = tokenHandler.CreateToken(securityTokenDescriptor);
        string? encodedToken = tokenHandler.WriteToken(token);

        return encodedToken;
    }

    private static List<Claim> ObterClaims(Usuario usuario)
    {
        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, usuario.Email.Valor),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Nbf, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        ];

        if (usuario.Perfil == PerfilUsuario.Administrador)
            claims.Add(new Claim(ClaimTypes.Role, RoleNames.Admin));

        return claims;
    }
}
