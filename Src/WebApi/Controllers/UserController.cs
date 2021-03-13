using Application.User.Commands.Login;
using Domain.Identity.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    
    public class UserController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AuthResult>> Authenticate(AuthenticateCommand command)
        {
            var value = await Mediator.Send(command);

            return base.Ok(value);
        }
    }
}
