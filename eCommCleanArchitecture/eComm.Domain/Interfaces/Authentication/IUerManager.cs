using eComm.Domain.Entities.Identitys;
using System.Security.Claims;

namespace eComm.Domain.Interfaces.Authentication
{
    public interface IUerManager
    {
        Task<bool> CreateUser(ApplicationUser user);
        Task<bool> UpdateUser(ApplicationUser user);
        Task<bool> LoginUser(ApplicationUser user);
        Task<ApplicationUser?> GetUserByEmail(string email);
        Task<ApplicationUser> GetUserById(string id);
        Task<IEnumerable<ApplicationUser>?> GetAllUser();
        Task<int> RemoveUserByEmail(string email);
        Task<List<Claim>> GetUserClaim(string email);
    }
}
