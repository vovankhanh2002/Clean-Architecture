using eComm.Application.DTOs;
using eComm.Application.DTOs.Identites;
namespace eComm.Application.Services.Interfaces.Authentication
{
    public interface IAuthencationService
    {
        Task<ServiceResponse> CreateUser(CreateUser user);
        Task<LoginResponse> LoginUser(LoginUser login);
        Task<LoginResponse> ReviceToken(string refreshToken);
    }
}
