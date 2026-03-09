namespace FCG.Users.Application.Identidade.DTOs;

public record UsuarioTokenDto(
    Guid Id,
    string Nome,
    string Email,
    byte Perfil
);
