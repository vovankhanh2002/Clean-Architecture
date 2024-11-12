using eComm.Domain.Entities.Identitys;
using eComm.Domain.Interfaces.Authentication;
using eComm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eComm.Infrastructure.Repositories.Authentication
{
    public class TokenManagerment(eCommDbcontext eCommDbcontext, IConfiguration config) : ITokenManager
    {
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            eCommDbcontext.refreshTokens.Add(new RefreshToken()
            {
                UserId = userId,
                Token = refreshToken
            });
            return await eCommDbcontext.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);
            var token = new JwtSecurityToken(
                issuer: config["JWT: Issuer"],
                audience: config["JWT: Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomBytes = new byte[byteSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);

            }
            return Convert.ToBase64String(randomBytes);
        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken != null)
                return jwtToken.Claims.ToList();
            else
                return [];
        }

        public async Task<string> GetUserIdByRefreshToken(string refreshToken)
            => ( await eCommDbcontext.refreshTokens.FirstOrDefaultAsync(i => i.Token == refreshToken))!.UserId;
     
        public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            var user = await eCommDbcontext.refreshTokens.FirstOrDefaultAsync(i => i.Token == refreshToken);
            if (user == null) return -1; 
            user!.Token = refreshToken;
            return await eCommDbcontext.SaveChangesAsync();
        }

        public async Task<bool> VailidateRefreshToken(string refreshToken)
        {
            var user = await eCommDbcontext.refreshTokens.FirstOrDefaultAsync(i => i.Token == refreshToken);
            return user != null;
        }
    }
}
