using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);
        private readonly IUserData _userData;
        private readonly IConfiguration _configuration;

        public IdentityController(IUserData userData, IConfiguration configuration)
        {
            _userData = userData;
            _configuration = configuration;

        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenGenerationRequest request)
        {
            var user = await _userData.GetUserByEmail(request.Email);

            if (user == null || (!user.Active))
            {
                return BadRequest("User doesn't exist or inactive");
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Idsrv:Key"]);

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub, request.Email),
                    new(JwtRegisteredClaimNames.Email,request.Email)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.Add(TokenLifetime),
                    Issuer = _configuration["Idsrv:Issuer"],
                    Audience = _configuration["Idsrv:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

                return Ok(jwt);
            }
        }
    }
}