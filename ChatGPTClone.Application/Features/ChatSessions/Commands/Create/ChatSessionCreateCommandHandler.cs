using ChatGPTClone.Application.Common.Interfaces;
using MediatR;

namespace ChatGPTClone.Application.Features.ChatSessions.Commands.Create
{
    public class ChatSessionCreateCommandHandler : IRequestHandler<ChatSessionCreateCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public ChatSessionCreateCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Guid> Handle(ChatSessionCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
