namespace FCG.Users.Application.Identidade.DTOs;

public record LogoutRequest(
    Guid RefreshToken
);
