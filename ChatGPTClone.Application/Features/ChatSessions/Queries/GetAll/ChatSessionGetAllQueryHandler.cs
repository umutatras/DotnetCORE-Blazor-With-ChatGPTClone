using ChatGPTClone.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll
{
    public class ChatSessionGetAllQueryHandler : IRequestHandler<ChatSessionGetAllQuery, List<ChatSessionGetAllDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public ChatSessionGetAllQueryHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public Task<List<ChatSessionGetAllDto>> Handle(ChatSessionGetAllQuery request, CancellationToken cancellationToken)
        {
            return _context.ChatSessions.AsNoTracking().Where(x => x.AppUserId == _currentUserService.UserId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => ChatSessionGetAllDto.MapFromChatSession(x)).ToListAsync(cancellationToken);
        }
    }
}
