using ChatGPTClone.Domain.Common;
using ChatGPTClone.Domain.Enums;
using ChatGPTClone.Domain.ValueObjects;

namespace ChatGPTClone.Domain.Entities
{
    public sealed class ChatSession : EntityBase
    {
        public string Title { get; set; }
        public GptModelType Model { get; set; }
        //Eğer bu projede list tanımlanmış ise json olarak tutulacaktır.
        public List<ChatMessage> Messages { get; set; } = [];
        public List<ChatThread> Threads { get; set; } = [];

        public Guid AppUserId { get; set; }
        //Eğer bu projede ICollection tanımlanmış ise ilişkiyi temsil edecektir.
        //public ICollection<ChatMessage> Messages { get; set; }
    }
}
