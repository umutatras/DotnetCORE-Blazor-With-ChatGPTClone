using ChatGPTClone.Application.Features.ChatSessions.Commands.Create;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTClone.WebApi.Controllers
{

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
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsyncc(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediatr.Send(new ChatSessinGetByIdQuery(id), cancellationToken));

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ChatSessionCreateCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediatr.Send(command, cancellationToken));  
        }
    }
}
