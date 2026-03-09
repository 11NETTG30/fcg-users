namespace FCG.Users.Application.DTOs;

public record LogoutRequest(
    Guid RefreshToken
);
