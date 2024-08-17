using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTClone.Domain.ValueObjects
{
    public sealed class ChatThread
    {
        public string Id { get; set; }
        public List<ChatMessage> Messages { get; set; } =
        [];
        public DateTimeOffset CreatedOn { get; set; }
    }
}
