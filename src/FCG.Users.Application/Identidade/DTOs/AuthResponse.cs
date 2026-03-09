namespace FCG.Users.Application.Identidade.DTOs;

public record AuthResponse(
    string AccessToken,
    Guid RefreshToken,
    DateTime ExpiracaoAccessToken,
    DateTime ExpiracaoRefreshToken,
    UsuarioTokenDto Usuario
);
