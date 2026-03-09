using FCG.Users.Application.Identidade.DTOs;
using FCG.Users.Domain.Identidade.ValueObjects;
using FluentValidation;

namespace FCG.Users.Application.Identidade.Validators;

public sealed class CriarUsuarioRequestValidator : AbstractValidator<CriarUsuarioRequest>
{
    public CriarUsuarioRequestValidator()
    {
        RuleFor(request => request.Nome)
            .NotEmpty()
            .Length(2, 100);

        RuleFor(request => request.Email)
            .NotEmpty()
            .MaximumLength(256)
            .Matches(Email.emailRegex)
            .WithMessage("'{PropertyName}' é um endereço de email inválido.");

        RuleFor(request => request.Senha)
            .SenhaValida();

        RuleFor(request => request.ConfirmacaoSenha)
            .NotEmpty()
            .Equal(request => request.Senha)
            .WithMessage("As senhas não conferem");
    }
}
