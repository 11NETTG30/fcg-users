using FCG.Users.Application.Identidade.DTOs;
using FluentValidation;

namespace FCG.Users.Application.Identidade.Validators;

public class LogoutRequestValidator : AbstractValidator<RefreshRequest>
{
    public LogoutRequestValidator()
    {
        RuleFor(request => request.RefreshToken)
            .NotEmpty();
    }
}
