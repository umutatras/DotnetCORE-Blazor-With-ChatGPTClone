using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById
{
    public class ChatSessionGetByIdQueryHandler : IRequestHandler<ChatSessinGetByIdQuery, ChatSessionGetByIdDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "ChatSessionGetById_";
        private readonly MemoryCacheEntryOptions _cacheOptions;
        public ChatSessionGetByIdQueryHandler(ICurrentUserService currentUserService, IApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                .SetPriority(CacheItemPriority.High);

        }
        public async Task<ChatSessionGetByIdDto> Handle(ChatSessinGetByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"{CacheKey}{request.Id}";
            if (_memoryCache.TryGetValue(cacheKey, out ChatSessionGetByIdDto cachedResult))
                return cachedResult;
            var chatSession = await _context.ChatSessions
                  .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var chatSessioMap=ChatSessionGetByIdDto.MapFromChatSession(chatSession);
            _memoryCache.Set(cacheKey, chatSessioMap,_cacheOptions);
            return ChatSessionGetByIdDto.MapFromChatSession(chatSession);


        }
    }
}
