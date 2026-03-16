using FCG.Users.Application.Security;
using FCG.Users.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FCG.Users.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route(".well-known")]
public sealed class JwksController(IJwksService jwksService, IOptions<JwtSettings> jwtSettings) : ControllerBase
{
    private readonly IJwksService _jwksService = jwksService;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    [HttpGet("jwks.json")]
    public IActionResult ObterChavesPublicas()
    {
        return Ok(_jwksService.ObterChavesPublicas());
    }

    [HttpGet("openid-configuration")]
    public IActionResult ObterConfiguracaoOidc()
    {
        string jwksUri = $"{Request.Scheme}://{Request.Host}/.well-known/jwks.json";
        return Ok(new { issuer = _jwtSettings.Emissor, jwks_uri = jwksUri });
    }
}
