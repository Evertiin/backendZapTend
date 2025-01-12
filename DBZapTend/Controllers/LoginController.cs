using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DBZapTend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DbzapContext _context;

        public LoginController(DbzapContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task <ActionResult<User>> Login(User user,string email,string idAuthentication)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(idAuthentication))
            {
                return BadRequest("Email e Id são obrigatórios.");
            }
            var User = await _context.Users
               .FirstOrDefaultAsync(u => u.Email == email && u.IdAutentication == idAuthentication);
       
            if (user != null)
            {
                var token = GenerateTokenJWT();
                return Ok(new { token,user});
            }
            else
            {
                return NotFound("Usuário não cadastrado ou credenciais incorretas."); 
            }
            
        }

        private string GenerateTokenJWT()
        {
            string secretKey = Program.secretKey;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim("login", "admin"),
            };
            var token = new JwtSecurityToken(
                issuer: "StarAnyTech",
                audience: "ZapTend",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
