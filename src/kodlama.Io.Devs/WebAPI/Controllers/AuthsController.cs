using Application.Features.Authorizations.Commands.RegisterUser;
using Application.Features.Authorizations.Dtos;
using Application.Features.Authorizations.Queries.LoginUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterUserCommand registerCommand)
        {
            var result = Mediator.Send(registerCommand);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            LoginUserDto loginUserDto = await Mediator.Send(loginUserCommand);
            return Ok(loginUserDto);
        }
    }
}
