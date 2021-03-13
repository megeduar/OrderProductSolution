using Domain.Identity.Response;
using System.Threading.Tasks;

namespace Application.Common.Interface
{
    public interface IUserManager
    {
        Task<AuthResult> Authenticate(string userName, string password);
    }
}
