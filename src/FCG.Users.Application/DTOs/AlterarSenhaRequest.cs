namespace FCG.Users.Application.DTOs;

public record AlterarSenhaRequest(
    string SenhaAtual,
    string NovaSenha,
    string ConfirmacaoNovaSenha
);
