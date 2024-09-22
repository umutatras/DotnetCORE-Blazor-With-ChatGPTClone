using ChatGPTClone.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll
{
    public class ChatSessionGetAllQueryHandler : IRequestHandler<ChatSessionGetAllQuery, List<ChatSessionGetAllDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "ChatSessionGetAll_";
        private readonly MemoryCacheEntryOptions _cacheOptions;
        public ChatSessionGetAllQueryHandler(ICurrentUserService currentUserService, IApplicationDbContext context, IMemoryCache memoryCache)
        {
            _currentUserService = currentUserService;
            _context = context;
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                .SetPriority(CacheItemPriority.High);

        }

        public async Task<List<ChatSessionGetAllDto>> Handle(ChatSessionGetAllQuery request, CancellationToken cancellationToken)
        {
            var cacheKey=$"{CacheKey}{_currentUserService.UserId}";

            if (_memoryCache.TryGetValue(cacheKey, out List<ChatSessionGetAllDto> cachedResult))
                return cachedResult;   

            var chatSessions= await _context.ChatSessions.AsNoTracking().Where(x => x.AppUserId == _currentUserService.UserId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => ChatSessionGetAllDto.MapFromChatSession(x)).ToListAsync(cancellationToken);

            _memoryCache.Set(cacheKey, chatSessions,_cacheOptions);
            return chatSessions;
        }
    }
}
