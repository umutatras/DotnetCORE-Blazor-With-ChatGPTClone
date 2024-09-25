using ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;

namespace ChatGPTClone.Application.Common.Interfaces
{
    public interface IChatSessionCacheService
    {
        Task<List<ChatSessionGetAllDto>> GetAllAsync(CancellationToken token);

        Task<ChatSessionGetByIdDto>GetByIdAsync(Guid id,CancellationToken token);

        void Remove(Guid id);   

        Task<bool> ExistsAsync(Guid id,CancellationToken cancellationToken);
    }
}
