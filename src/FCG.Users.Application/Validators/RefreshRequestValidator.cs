using FCG.Users.Application.DTOs;
using FluentValidation;

namespace FCG.Users.Application.Validators;

public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
{
    public RefreshRequestValidator()
    {
        RuleFor(request => request.RefreshToken)
            .NotEmpty();
    }
}
