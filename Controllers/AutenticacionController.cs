using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiWeb.Modelos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ApiWeb.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;
        public AutenticacionController(IConfiguration config)
        {
            secretKey=config.GetSection("settings").GetSection("secretkey").ToString();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Validar([FromBody] Usuario request)
        {
            if(request.username == "admin" && request.password == "admin")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier,request.username));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject=claims, Expires=DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256Signature)

                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string tokencreado = tokenHandler.WriteToken(tokenConfig);
                return StatusCode(StatusCodes.Status200OK, new {token=tokencreado});
            }
            else { return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" }); }
        }
    }
}
