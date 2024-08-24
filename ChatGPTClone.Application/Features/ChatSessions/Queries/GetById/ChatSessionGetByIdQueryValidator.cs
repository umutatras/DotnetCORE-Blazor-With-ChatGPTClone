using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById
{
    public class ChatSessionGetByIdQueryValidator:AbstractValidator<ChatSessinGetByIdQuery>
    {
        private readonly IApplicationDbContext _context;
        public ChatSessionGetByIdQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Id)
                .NotEmpty()
                .NotNull()
                .MustAsync(BeValidIdAsync);
        }
        private Task<bool> BeValidIdAsync(Guid id,CancellationToken cancellationToken)
        {
            return _context.ChatSessions.AnyAsync(x=>x.Id == id,cancellationToken);
        }
    }
}
