using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;

namespace ChatGPTClone.Application.Features.Auth.Commands.Register
{
    public class AuthRegisterCommandValidator : AbstractValidator<AuthRegisterCommand
        >
    {
        private readonly IIdentityService _identityService;

        public AuthRegisterCommandValidator(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public AuthRegisterCommandValidator()
        {
            RuleFor(x => x.Email)
       .NotEmpty()
       .EmailAddress()
       .MustAsync(CheckEmailExistAsync)
       .WithMessage("The mail is already in use");

            RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

            RuleFor(x => x.FirstName)
            .MaximumLength(50);

            RuleFor(x => x.LastName)
            .MaximumLength(50);
            
        }
        private async Task<bool> CheckEmailExistAsync(string email,CancellationToken cancellation)
        {
            return !await _identityService.CheckEmailExistsAsync(email, cancellation);
        }
    }
}
