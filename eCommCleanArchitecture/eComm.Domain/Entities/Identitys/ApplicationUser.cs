using Microsoft.AspNetCore.Identity;
namespace eComm.Domain.Entities.Identitys
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {  get; set; } = string.Empty;
    }
}
