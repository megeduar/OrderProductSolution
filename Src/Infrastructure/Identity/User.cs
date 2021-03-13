using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Identity
{
    public class User : IdentityUser
    {
        #region Properties
        public string Name { get; set; }
        public string LastName { get; set; }
        #endregion
    }
}
