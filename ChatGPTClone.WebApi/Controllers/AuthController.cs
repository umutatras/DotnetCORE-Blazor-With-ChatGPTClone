using ChatGPTClone.Application.Features.Auth.Commands.Login;
using ChatGPTClone.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        public AuthController(ISender mediator):base(mediator)
        {
            
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthLoginCommand command,CancellationToken cancellationToken)
        {
            return Ok(await Mediatr.Send(command, cancellationToken));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthRegisterCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediatr.Send(command, cancellationToken));
        }
    }
}
