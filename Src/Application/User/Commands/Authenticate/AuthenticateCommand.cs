using Application.Common.Interface;
using Domain.Identity.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.Commands.Login
{
    public class AuthenticateCommand : IRequest<AuthResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<AuthenticateCommand, AuthResult>
        {
            private readonly IUserManager _userManager;

            public Handler(IUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<AuthResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
            {
                return await _userManager.Authenticate(request.Username, request.Password);

                throw new ExceptionHandler.
                    ExceptionHandler(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
