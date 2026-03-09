using FCG.Users.Application.DTOs;
using FluentValidation;

namespace FCG.Users.Application.Validators;

public class LogoutRequestValidator : AbstractValidator<RefreshRequest>
{
    public LogoutRequestValidator()
    {
        RuleFor(request => request.RefreshToken)
            .NotEmpty();
    }
}
