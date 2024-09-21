using ChatGPTClone.Application.Features.ChatMessages.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTClone.WebApi.Controllers
{
    [Authorize]
    public class ChatMessagesController : ApiControllerBase
    {
        public ChatMessagesController(ISender mediatr) : base(mediatr)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatMessageCreateCommand command, CancellationToken token) => Ok(await Mediatr.Send(command, token));
    }
}
