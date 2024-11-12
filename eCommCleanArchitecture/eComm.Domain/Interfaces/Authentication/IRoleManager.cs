using eComm.Domain.Entities.Identitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComm.Domain.Interfaces.Authentication
{
    public interface IRoleManager
    {
        Task<string> GetUserRole(string email);
        Task<bool> AddUerToRole(ApplicationUser user, string roleName);
    }
}
