using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using web_api_dotnet6.Models;

namespace web_api_dotnet6.Controllers
{
    public class AuthController : Controller
    {
        private IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("Login")]
        public IActionResult LoginRequest([FromBody] Login login)
        {
            string user = "testuser";
            string pass = "123456";
            try
            {
                if (string.IsNullOrEmpty(login.username) || string.IsNullOrEmpty(login.password))
                {
                    return BadRequest("Username and/or Password not specified");
                }
                if (login.username == user && login.password == pass)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                      _config["Jwt:Issuer"],
                      null,
                      expires: DateTime.Now.AddMinutes(120),
                      signingCredentials: credentials);


                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(Sectoken)
                    });
                }
            }
            catch
            {
                return BadRequest("An error occurred in generating the token");
            }
            return Unauthorized();
        }

    }
}
