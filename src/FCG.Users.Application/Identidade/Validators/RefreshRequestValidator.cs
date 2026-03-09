using FCG.Users.Application.Identidade.DTOs;
using FluentValidation;

namespace FCG.Users.Application.Identidade.Validators;

public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
{
    public RefreshRequestValidator()
    {
        RuleFor(request => request.RefreshToken)
            .NotEmpty();
    }
}
