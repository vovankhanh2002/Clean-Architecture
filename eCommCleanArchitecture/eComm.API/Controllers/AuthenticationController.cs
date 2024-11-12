using eComm.Application.DTOs.Identites;
using eComm.Application.Services.Implementations.Authentication;
using eComm.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eComm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthencationService authencationService) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            var result = await authencationService.CreateUser(user);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUser user)
        {
            var result = await authencationService.LoginUser(user);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        [HttpGet("refreshToken/{refreshToken}")]
        public async Task<IActionResult> ReviveToken(string refreshToken)
        {
            var result = await authencationService.ReviceToken(refreshToken);
            return result.Flag ? Ok(result) : BadRequest(result);

        }
    }
}
