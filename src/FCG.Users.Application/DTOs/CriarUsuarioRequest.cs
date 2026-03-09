namespace FCG.Users.Application.DTOs;

public record CriarUsuarioRequest(
    string Nome,
    string Email,
    string Senha,
    string ConfirmacaoSenha
);
