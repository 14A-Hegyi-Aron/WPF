using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReceptionHour.WebAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromForm] string loginName, [FromForm] string password)
        {
            return this.Run(() =>
            {
                if (loginName == "admin" && password == "admin")
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes(this.GetConfigValue("JWT:Key"));
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Expires = DateTime.Now.AddHours(1),
                        SigningCredentials = new SigningCredentials
                        (
                            new SymmetricSecurityKey(tokenKey),
                            SecurityAlgorithms.HmacSha256Signature
                        ),
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, loginName)
                        })
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return Ok(new
                    {
                        token = tokenHandler.WriteToken(token)
                    });
                }
                else
                {
                    return Unauthorized("Hibás felhasználónév vagy jelszó!");
                }
            });
        }
    }
}
