using CarWorldModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace COMP_584_MyServer
{
    public class JWTHandler(UserManager<CarWorldModelUser> userManager, IConfiguration configuration)
    {
        public async Task<JwtSecurityToken> GenerateToken(CarWorldModelUser user)
        {
            return new JwtSecurityToken
            (
                issuer: configuration["JwtSettings:validIssuer"],
                audience: configuration["JwtSettings:validAudience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JwtSettings:ExpiryInMinutes"])),
                claims: await GetClaimsAsync(user),
                signingCredentials: GetSigningCredentials()
            );

        }

        private SigningCredentials GetSigningCredentials()
        {
            byte[] key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!);
            SymmetricSecurityKey signingKey = new(key);
            return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(CarWorldModelUser user)
        {
            List<Claim> claims = [new Claim(ClaimTypes.Name, user.UserName!)];
            //claims.AddRange(await userManager.GetRolesAsync(user).select(role => new Claim(ClaimTypes.Role, role)));
            foreach (var role in await userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
