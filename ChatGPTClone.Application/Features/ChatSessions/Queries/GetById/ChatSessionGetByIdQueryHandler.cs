using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById
{
    public class ChatSessionGetByIdQueryHandler : IRequestHandler<ChatSessinGetByIdQuery, ChatSessionGetByIdDto>
    {
        private readonly IChatSessionCacheService _cacheService;
        public  Task<ChatSessionGetByIdDto> Handle(ChatSessinGetByIdQuery request, CancellationToken cancellationToken)
        {

            return  _cacheService.GetByIdAsync(request.Id, cancellationToken);

        }
    }
}
