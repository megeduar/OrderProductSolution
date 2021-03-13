using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Identity;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class Roles
    {
        public const string ADMINISTRATOR = "Administrator";
        public const string SALES = "Sales";
        public const string CLIENT = "Client";
    }

    public class UserSeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();

            await RolesAsync(roleManager);

            var userManager = services
                .GetRequiredService<UserManager<User>>();

            await UserManagerAsync(userManager);
        }

        private static async Task UserManagerAsync(UserManager<User> userManager)
        {
            var AdminUser = new User
            {
                Name = "Josehp",
                LastName = "Jan",
                UserName = "Admin",
                Email = "admin@mx.local",
            };

            await userManager.CreateAsync(AdminUser, "@@22Secure");
            await userManager.AddToRoleAsync(AdminUser, Roles.ADMINISTRATOR);

            var SalesUser = new User
            {
                Name = "Philish",
                LastName = "Jan",
                UserName = "Sales",
                Email = "sales@mx.local",
            };

            await userManager.CreateAsync(SalesUser, "@@22Secure");
            await userManager.AddToRoleAsync(SalesUser, Roles.SALES);

            var ClientUser = new User
            {
                Name = "Patrick",
                LastName = "Jan",
                UserName = "Client",
                Email = "client@mx.local",
            };

            await userManager.CreateAsync(ClientUser, "@@22Secure");
            await userManager.AddToRoleAsync(ClientUser, Roles.CLIENT);
        }

        private static async Task RolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(
                    new IdentityRole(Roles.ADMINISTRATOR));

            await roleManager.CreateAsync(
                    new IdentityRole(Roles.SALES));

            await roleManager.CreateAsync(
                    new IdentityRole(Roles.CLIENT));
        }
    }
}
