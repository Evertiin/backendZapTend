using DBZapTend.DTO;
using DBZapTend.Logs;
using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBZapTend.Controllers
{
    //[Authorize(Roles = "User,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                var users = await _repository.GetUsers();
                await Log.LogToFile("log_", "COD:1001-2 ,Usuários coletado com sucesso");
                return Ok(new { Message = "COD:1001-2 ,Usuários coletado com sucesso", Users = users });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1001-5,Erro interno ao buscar usuários: {ex.Message}");
                return StatusCode(500, $"COD:1001-5,Erro interno ao buscar usuários");
            }
        }
        //[Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUsers([FromBody] UpdateUserDto userDto)
        {
            if (userDto == null)
            {
                await Log.LogToFile("log_", $"COD:1001-4 ,Dados inválidos");
                return BadRequest("COD:1001-4 ,Dados inválidos");
            }

            try
            {
                var user = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    IdAutentication = userDto.IdAutentication,
                    Address = userDto.Address,
                    CpfCnpj = userDto.CpfCnpj,
                    Telephone = userDto.Telephone ?? 0,
                    Role = userDto.Role
                };

                var createdUser = await _repository.CreateUser(user);

                await Log.LogToFile("log_", $"COD:1001-2 ,Usuário criado com sucesso");
                return Ok(new { Message = "COD:1001-2 ,Usuário criado com sucesso", Users = user });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1001-5 ,Erro interno do servidor: {ex.Message}");
                return StatusCode(500, "COD:1001-5,Erro interno ao criar usuário");
            }
        }

        [HttpGet("{id:minlength(3):maxlength(99)}")]
        public async Task<ActionResult<User>> GetUserId(string id)
        {
            try
            {
                var user = await _repository.GetUser(id);

                if (user == null)
                {
                    await Log.LogToFile("log_", $"COD:1001-4 ,Usuário não encontrado");
                    return NotFound("COD:1001-4, Usuário não encontrado");
                }
                await Log.LogToFile("log_", "COD:1001-2 ,Usuário coletado com sucesso");
                return Ok(new { Message = "COD:1001-2 ,Usuário coletado com sucesso", Users = user });
               
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1001-5 ,Erro interno do servidor ao buscar usuário: {ex.Message}");
                return StatusCode(500, "COD:1001-5,Erro interno do servidor ao buscar usuário");
                
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPatch("{id:minlength(3):maxlength(100)}")]
        public async Task<ActionResult<User>> UpdateUser(string id, [FromBody] UpdateUserDto user)
        {
            try
            {
                var findUser = await _repository.GetUser(id);

                if (findUser is null)
                {
                    await Log.LogToFile("log_", $"COD:1001-4 Usuário não encontrado");
                    return NotFound("COD:1001-4,Usuário não encontrado");

                }
                await Log.LogToFile("log_", "COD:1001-2 ,Usuário coletado com sucesso");
                if (!string.IsNullOrWhiteSpace(user.Name))
                {
                    findUser.Name = user.Name;
                }

                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    findUser.Email = user.Email;
                }
                if (!string.IsNullOrWhiteSpace(user.Role))
                {
                    findUser.Role = user.Role;
                }

                if (!string.IsNullOrWhiteSpace(user.CpfCnpj));
                {
                    findUser.CpfCnpj = user.CpfCnpj;
                }

                if (user.Telephone.HasValue)
                {
                    findUser.Telephone = user.Telephone.Value;
                }

                if (!string.IsNullOrWhiteSpace(user.Address))
                {
                    findUser.Address = user.Address;
                }

                await _repository.UpdateUsers(findUser);
                await Log.LogToFile("log_", $"COD:1001-2 ,Atualizado com sucesso");
                return Ok("COD:1001-2,Atualizado com sucesso");
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1001-5 ,Erro interno do servidor ao atualizar usuário: {ex.Message}");
                return StatusCode(500, "COD:1001-5, Erro interno ao Atualizar Usuário");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{id:minlength(3):maxlength(100)}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            try
            {
                var deleteUser = await _repository.DeleteUser(id);

                if (deleteUser == null)
                {
                    await Log.LogToFile("log_", $"COD:1001-4 ,Usuário não encontrado");
                    return NotFound("COD:1001-4,Usuário não encontrado");
                }
                await Log.LogToFile("log_", $"COD:1001-2 ,Usuário deletado com sucesso");
                return Ok("COD:1001-2 ,Usuário deletado com sucesso");
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1001-5 ,Erro interno do servidor ao atualizar usuário: {ex.Message}");
                return StatusCode(500, "COD:1001-5, Erro interno ao Atualizar Usuário:");
            }
        }
    }
}