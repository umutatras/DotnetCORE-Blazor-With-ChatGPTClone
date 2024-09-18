using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.OpenAI;
using OpenAI.Interfaces;

namespace ChatGPTClone.Infrastructure.Services
{
    public class OpenAIManager : IOpenAiService
    {
        private readonly IOpenAIService _openAIService;
        private readonly ICurrentUserService _currentUserService;
        public OpenAIManager(IOpenAIService openAIService, ICurrentUserService currentUserService)
        {
            _openAIService = openAIService;
            _currentUserService = currentUserService;
        }

        public Task<OpenAIChatResponse> ChatAsync(OpenAIChatRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
