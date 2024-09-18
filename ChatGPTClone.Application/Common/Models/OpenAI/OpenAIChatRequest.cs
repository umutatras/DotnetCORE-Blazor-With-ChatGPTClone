using ChatGPTClone.Domain.Enums;

namespace ChatGPTClone.Application.Common.Models.OpenAI
{
    public class OpenAIChatRequest
    {
        public GptModelType Model { get; set; }
        public string Message { get; set; }
        public List<ChatMessageType> Messages { get; set; }
    }
}
