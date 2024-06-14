using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteNivelamento.Interfaces;
using TesteNivelamento.Models;

namespace TesteNivelamento.Controllers
{
    [ApiController]
    [Route("/api/auth/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly string _key;


        public LoginController(IUserService userService, IOptions<APPSettings> appSettings)
        {
            _userService = userService;
            _key = appSettings.Value.Secret;

        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User userParam)
        {
            var loginAuthentificate = _userService.Authetificate(userParam.UserName, userParam.Password);

            if (loginAuthentificate == null)
            {
                return Unauthorized();
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor

            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, loginAuthentificate.Id.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}
