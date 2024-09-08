using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTClone.Application.Features.Auth.Commands.VerifyEmail
{
    public class AuthVerifyEmailCommandValidator:AbstractValidator<AuthVerifyEmailCommand>
    {
        private readonly IIdentityService _identityService;

        public AuthVerifyEmailCommandValidator(IIdentityService identityService)
        {
            _identityService = identityService;

            RuleFor(x => x.Email)
            .NotEmpty().EmailAddress()
            .MustAsync(EmailExists).WithMessage("Email not found.");

            RuleFor(x => x.Token)
                 .NotEmpty()
                 .MinimumLength(20)
                 .WithMessage("Token is invalid.");
        }

        private Task<bool> EmailExists(string email, CancellationToken cancellationToken)
        {
            return _identityService.CheckEmailExistsAsync(email, cancellationToken);
        }
    }
}
