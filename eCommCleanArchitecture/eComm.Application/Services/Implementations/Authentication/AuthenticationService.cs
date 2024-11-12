using AutoMapper;
using eComm.Application.DTOs;
using eComm.Application.DTOs.Identites;
using eComm.Application.Services.Interfaces.Authentication;
using eComm.Application.Validation;
using eComm.Application.Validation.Authentication;
using eComm.Domain.Entities.Identitys;
using eComm.Domain.Interfaces.Authentication;
using FluentValidation;
namespace eComm.Application.Services.Implementations.Authentication
{
    public class AuthenticationService(ITokenManager tokenManager,
        IUerManager uerManager,
        IRoleManager roleManager,
        IValidator<CreateUser> validatorCreate,
        IValidator<LoginUser> validatorLogin,
        IValidationService validationService,
        IMapper mapper
        ) : IAuthencationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var _Validation = await validationService.ValidateAsync(user, validatorCreate);
            if (!_Validation.Flag) return _Validation;

            var mapperUser = mapper.Map<ApplicationUser>(user);
            mapperUser.FullName = user.Fullname;
            mapperUser.PasswordHash = user.Password;
            mapperUser.UserName = user.Email;

            var result = await uerManager.CreateUser(mapperUser);
            if (!result) return new ServiceResponse{Message = "Error not create"};
            
            var _user = await uerManager.GetUserByEmail(user.Email);
            var users = await uerManager.GetAllUser();

            bool assignedResult = await roleManager.AddUerToRole(_user!, users!.Count() > 1 ? "User" : "Admin");

            if (!assignedResult)
            {
                var removeUser = await uerManager.RemoveUserByEmail(user.Email);
                if (removeUser <= 0) 
                {
                    return new ServiceResponse { Message = "Error create account" };
                }
            }
            return new ServiceResponse { Flag = true, Message = "Create account" };
        }

        public async Task<LoginResponse> LoginUser(LoginUser login)
        {
            var _validationResult = await validationService.ValidateAsync(login, validatorLogin); 
            if (!_validationResult.Flag)
                return new LoginResponse(Message: _validationResult.Message);

            var mappedModel = mapper.Map<ApplicationUser>(login);

            mappedModel.PasswordHash = login.Password;

            bool loginResult = await uerManager.LoginUser(mappedModel);

            if (!loginResult)
                return new LoginResponse(Message: "Email not found or invalid credentials");

            var _user = await uerManager.GetUserByEmail(login.Email);

            var claims = await uerManager.GetUserClaim(_user!.Email!);

            string jwtToken = tokenManager.GenerateToken(claims);

            string refreshToken = tokenManager.GetRefreshToken();

            int saveTokenResult = await tokenManager.AddRefreshToken(_user.Id, refreshToken);

            return saveTokenResult <= 0 ? new LoginResponse(Message: "Internal error occurred while authenticating") :
                new LoginResponse(Flag: true, Token: jwtToken, RefreshToken: refreshToken);
        }

        public async Task<LoginResponse> ReviceToken(string refreshToken)
        {
            bool validateTokenResult = await tokenManager.VailidateRefreshToken(refreshToken);
            if (!validateTokenResult)
                return new LoginResponse(Message: "Invalid token");

            string userId = await tokenManager.GetUserIdByRefreshToken(refreshToken);

            ApplicationUser? user = await uerManager.GetUserById(userId);

            var claims = await uerManager.GetUserClaim(user!.Email!);

            string newJwtToken = tokenManager.GenerateToken(claims);

            string newRefreshToken = tokenManager.GetRefreshToken();

            await tokenManager.UpdateRefreshToken(userId, newRefreshToken);

            return new LoginResponse(Flag: true, Token: newJwtToken, RefreshToken: newRefreshToken);
        }
    }
}
