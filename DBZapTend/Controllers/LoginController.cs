using DBZapTend.DTO;
using DBZapTend.Logs;
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
                    await Log.LogToFile("log_", "COD:1011-4 ,Email e Id são obrigatórios");
                    return BadRequest("COD:1011-4 ,Email e Id são obrigatórios.");
                }

                var userFromDb = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email && u.IdAutentication == request.IdAuthentication);

                if (userFromDb == null)
                {
                    await Log.LogToFile("log_", $"COD:1011-4 ,Usuário não cadastrado ou credenciais incorretas");
                    return NotFound("COD:1011-4 ,Usuário não cadastrado ou credenciais incorretas.");
                }

                string token;
                token = GenerateTokenJWT(userFromDb.Role);

                /*if (userFromDb.Role == "Admin"){
                
                    token = GenerateTokenAdminJWT();
                    await Log.LogToFile("log_", $"COD:1011-2 ,Login de admin realizado com sucesso");
                }
                else
                {
                    token = GenerateTokenUserJWT(userFromDb.Role);
                    await Log.LogToFile("log_", $"COD:1011-2 ,Login de usuário realizado com sucesso");
                }*/
                await Log.LogToFile("log_", $"COD:1011-2 ,Login de admin realizado com sucesso");
                return Ok(new { token, user = userFromDb });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1011-5 ,Erro interno ao realizar login: {ex.Message}");
                return StatusCode(500, $"COD:1011-5 ,Erro interno do servidor");
            }
        }
    
        private string GenerateTokenJWT(string role)
        {
            try
            {
                string secretKey = Program.secretKey;
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var userClaims = new List<Claim>
                {
                  //new Claim(ClaimTypes.Name, "user"),
                  new Claim(ClaimTypes.Role, role)
                };

                var tokenUser = new JwtSecurityToken(
                    issuer: "StarAnyTech",
                    audience: "ZapTend",
                    claims: userClaims,
                    expires: DateTime.Now.AddHours(1),
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