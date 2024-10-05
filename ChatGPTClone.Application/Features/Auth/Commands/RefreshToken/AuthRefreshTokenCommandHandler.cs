using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.General;
using MediatR;

namespace ChatGPTClone.Application.Features.Auth.Commands.RefreshToken
{
    public sealed class AuthRefreshTokenCommandHandler : IRequestHandler<AuthRefreshTokenCommand, ResponseDto<AuthRefreshTokenResponse>>
    {
        private readonly IIdentityService _identityService;

        public AuthRefreshTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public Task<ResponseDto<AuthRefreshTokenResponse>> Handle(AuthRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
