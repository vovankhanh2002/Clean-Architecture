using eComm.Domain.Entities.Identitys;
using eComm.Domain.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComm.Infrastructure.Repositories.Authentication
{
    public class RoleManagerment(UserManager<ApplicationUser> userManager) : IRoleManager
    {
        public async Task<bool> AddUerToRole(ApplicationUser user, string roleName) => (await userManager.AddToRoleAsync(user, roleName)).Succeeded;

        public async Task<string> GetUserRole(string email)
        {
            var _user = await userManager.FindByEmailAsync(email);
            return (await userManager.GetRolesAsync(_user!)).FirstOrDefault();
        }
    }
}
