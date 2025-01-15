using DBZapTend.DTO;
using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<ActionResult<User>> Login(LoginRequestDto request)
        {
            
            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.IdAuthentication))
                {
                    return BadRequest("Email e Id são obrigatórios.");
                }

                var userFromDb = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email && u.IdAutentication == request.IdAuthentication);

                if (userFromDb == null)
                {
                    return NotFound("Usuário não cadastrado ou credenciais incorretas.");
                }
                string token;
                if (request.Role == "Admin")
                {
                    token = GenerateTokenAdminJWT(); 
                }
                else
                {
                    token = GenerateTokenUserJWT(); 
                }

                // Retorna o token e as informações do usuário
                return Ok(new { token, user = userFromDb });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        private string GenerateTokenAdminJWT()
        {
            try
            {
                string secretKey = Program.secretKey;
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var adminClaims = new List<Claim>
                {
                  new Claim(ClaimTypes.Name, "adminUser"),
                  new Claim(ClaimTypes.Role, "Admin")
                };

                var tokenAdmin = new JwtSecurityToken(
                    issuer: "StarAnyTech",
                    audience: "ZapTend",
                    claims: adminClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(tokenAdmin);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar o token JWT.", ex);
            }
        }
        private string GenerateTokenUserJWT()
        {
            try
            {
                string secretKey = Program.secretKey;
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var userClaims = new List<Claim>
                {
                  new Claim(ClaimTypes.Name, "user"),
                  new Claim(ClaimTypes.Role, "User")
                };

                var tokenUser = new JwtSecurityToken(
                    issuer: "StarAnyTech",
                    audience: "ZapTend",
                    claims: userClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(tokenUser);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar o token JWT.", ex);
            }
        }
    }
}