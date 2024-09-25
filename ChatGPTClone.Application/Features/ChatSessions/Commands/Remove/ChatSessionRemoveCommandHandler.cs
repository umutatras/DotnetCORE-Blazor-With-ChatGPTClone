using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.General;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChatGPTClone.Application.Features.ChatSessions.Commands.Remove
{
    public class ChatSessionRemoveCommandHandler : IRequestHandler<ChatSessionRemoveCommand, ResponseDto<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IChatSessionCacheService _cacheService;

        public ChatSessionRemoveCommandHandler(IChatSessionCacheService cacheService, IApplicationDbContext dbContext)
        {
            _cacheService = cacheService;
            _dbContext = dbContext;
        }

        public async Task<ResponseDto<Guid>> Handle(ChatSessionRemoveCommand request, CancellationToken cancellationToken)
        {
            var chatSession = await _dbContext
          .ChatSessions
          .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            _dbContext.ChatSessions.Remove(chatSession);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _cacheService.Remove(request.Id);

            return new ResponseDto<Guid>(chatSession.Id, "Chat session was deleted successfully.");

        }

      
    }

}