using Api.Application.Features.Auth.Command.Login;
using Api.Application.Features.Auth.Command.RefreshToken;
using Api.Application.Features.Auth.Command.Register;
using Api.Application.Features.Auth.Command.Revoke;
using Api.Application.Features.Auth.Command.RevokeAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Api.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommandRequest request) 
        {
            await _mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandRequest request) 
         {
           var  response= await _mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }
        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request) 
        {
            var response = await _mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }
        [HttpPost]
        public async Task<IActionResult> Revoke(RevokeCommandRequest request) 
        {
            var response = await _mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpPost]
        public async Task<IActionResult> RevokeAll()
        {
            var response = await _mediator.Send(new RevokeAllCommandRequest()); //kullanıcıdan request alacaksak parametre olarak eklememize gerek yok
                                                                                //direkt olarak requeste istek atabilmek için sende metodu içinde classı newleriz
            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
