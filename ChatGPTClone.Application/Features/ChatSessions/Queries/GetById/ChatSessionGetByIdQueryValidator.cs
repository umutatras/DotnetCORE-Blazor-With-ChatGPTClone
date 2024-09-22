using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById
{
    public class ChatSessionGetByIdQueryValidator : AbstractValidator<ChatSessinGetByIdQuery>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public ChatSessionGetByIdQueryValidator(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;

            RuleFor(p => p.Id)
                .NotEmpty()
                .NotNull()
                .MustAsync(BeValidIdAsync)
                .WithMessage("The selected Chat was not found.");
            _currentUserService = currentUserService;
        }
        private Task<bool> BeValidIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.ChatSessions.AnyAsync(x => x.Id == id && x.AppUserId == _currentUserService.UserId, cancellationToken);
        }
    }
}
