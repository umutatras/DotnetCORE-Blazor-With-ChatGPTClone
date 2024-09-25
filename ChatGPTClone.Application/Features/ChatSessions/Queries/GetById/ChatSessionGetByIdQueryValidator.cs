using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById
{
    public class ChatSessionGetByIdQueryValidator : AbstractValidator<ChatSessinGetByIdQuery>
    {
        private readonly IChatSessionCacheService _chatSessionCacheService;
        public ChatSessionGetByIdQueryValidator(IChatSessionCacheService chatSessionCacheService)
        {

            RuleFor(p => p.Id)
                .NotEmpty()
                .NotNull()
                .MustAsync(BeValidIdAsync)
                .WithMessage("The selected Chat was not found.");
         
            _chatSessionCacheService = chatSessionCacheService;
        }
        private Task<bool> BeValidIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _chatSessionCacheService.ExistsAsync(id,cancellationToken);
        }
    }
}
