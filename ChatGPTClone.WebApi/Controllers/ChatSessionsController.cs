using ChatGPTClone.Application.Features.ChatSessions.Commands.Create;
using ChatGPTClone.Application.Features.ChatSessions.Commands.CreateRange;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTClone.WebApi.Controllers
{
    [Authorize]

    public class ChatSessionsController : ApiControllerBase
    {
        public ChatSessionsController(ISender mediatr) : base(mediatr)
        {
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediatr.Send(new ChatSessionGetAllQuery(), cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsyncc(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediatr.Send(new ChatSessinGetByIdQuery(id), cancellationToken));

        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ChatSessionCreateCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediatr.Send(command, cancellationToken));  
        }
        [HttpPost("range")]
        public async Task<IActionResult> CreateRangeAsync(ChatSessionCreateRangeCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediatr.Send(command, cancellationToken));
        }
    }
}
