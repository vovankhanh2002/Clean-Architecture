using eComm.Domain.Entities.Identitys;
using eComm.Domain.Interfaces.Authentication;
using eComm.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eComm.Infrastructure.Repositories.Authentication
{
    public class UserManagerment(UserManager<ApplicationUser> userManager, eCommDbcontext commDbcontext, IRoleManager roleManager) : IUerManager
    {
        public async Task<bool> CreateUser(ApplicationUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user != null) return false;
            return (await userManager.CreateAsync(user!, user!.PasswordHash!)).Succeeded;
        }

        public async Task<IEnumerable<ApplicationUser>?> GetAllUser() => await commDbcontext.Users.ToListAsync();

        public async Task<ApplicationUser?> GetUserByEmail(string email) => await userManager.FindByEmailAsync(email);

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var _user = await userManager.FindByIdAsync(id);
            return _user!;
        }

        public async Task<List<Claim>> GetUserClaim(string email)
        {
            var _user = await GetUserByEmail(email!);
            string? roleName = await roleManager.GetUserRole(_user!.Email!);
            List<Claim> claims = [
                new Claim("Fullname", _user!.FullName),
                new Claim (ClaimTypes.NameIdentifier, _user!.Id),
                new Claim(ClaimTypes.Email, _user.Email!),
                new Claim(ClaimTypes.Role, roleName!)
            ];
            return claims;
        }

        public async Task<bool> LoginUser(ApplicationUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user == null) return false;
            string? roleName = await roleManager.GetUserRole(_user!.Email!);
            if (roleName == null) return false;
            return await userManager.CheckPasswordAsync(_user, user.PasswordHash!);
        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var _user = await commDbcontext.Users.FirstOrDefaultAsync(i => i.Email == email);
            commDbcontext.Users.Remove(_user);
            return await commDbcontext.SaveChangesAsync();
        }

        public Task<bool> UpdateUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
