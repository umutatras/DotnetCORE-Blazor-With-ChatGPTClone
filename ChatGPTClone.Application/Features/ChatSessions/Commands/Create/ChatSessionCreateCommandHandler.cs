﻿using ChatGPTClone.Application.Common.Interfaces;
using MediatR;

namespace ChatGPTClone.Application.Features.ChatSessions.Commands.Create
{
    public class ChatSessionCreateCommandHandler : IRequestHandler<ChatSessionCreateCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public ChatSessionCreateCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(ChatSessionCreateCommand request, CancellationToken cancellationToken)
        {
            var chatSession=request.ToChatSession(_currentUserService.UserId);

            _context.ChatSessions.Add(chatSession);
            await _context.SaveChangesAsync(cancellationToken);
            return chatSession.Id;
        }
    }
}
