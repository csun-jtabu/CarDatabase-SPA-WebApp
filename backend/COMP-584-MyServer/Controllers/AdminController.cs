using CarWorldModel;
using COMP_584_MyServer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace COMP_584_MyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<CarWorldModelUser> userManager, JWTHandler jwtHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            CarWorldModelUser? carWorldModelUser = await userManager.FindByNameAsync(loginRequest.Username);
            if (carWorldModelUser == null)
            {
                return Unauthorized("Invalid username");
            }

            bool loginStatus = await userManager.CheckPasswordAsync(carWorldModelUser, loginRequest.Password);
            if (!loginStatus)
            {
                return Unauthorized("Invalid password");
            }

            JwtSecurityToken jwtToken = await jwtHandler.GenerateToken(carWorldModelUser);
            string stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Mom loves me",
                Token = stringToken
            });
        }
    }
}
