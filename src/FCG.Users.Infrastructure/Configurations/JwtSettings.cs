using System.ComponentModel.DataAnnotations;
using FCG.Users.Application.Security;

namespace FCG.Users.Infrastructure.Configurations;

public sealed class JwtSettings : ITokenSettings
{
    public const string ChaveId = "fcg-key-1";

    [Required]
    public required string ChavePrivadaRsaBase64 { get; init; }

    [Required]
    public required string Emissor { get; init; }

    [Required]
    public required string Audiencia { get; init; }

    [Range(5, 1440)]
    public short ExpiracaoAccessTokenMinutos { get; init; }

    [Range(1, 30)]
    public byte ExpiracaoRefreshTokenDias { get; init; }

    public bool HabilitarSegurancaDeReusoRefreshToken { get; init; } = false;
}
