using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBZapTend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                var users = await _repository.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar usuários: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUsers(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Name) ||
                    string.IsNullOrEmpty(user.Email) ||
                    string.IsNullOrEmpty(user.Password) ||
                    string.IsNullOrEmpty(user.Adress) ||
                    string.IsNullOrEmpty(user.IdAutentication))
                {
                    return BadRequest("Dados inválidos");
                }

                var createdUser = await _repository.CreateUser(user);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao criar usuário: {ex.Message}");
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
                    return NotFound("Usuário não encontrado");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar usuário por ID: {ex.Message}");
            }
        }

        [HttpPut("{id:minlength(3):maxlength(100)}")]
        public async Task<ActionResult<User>> UpdateUser(string id, User user)
        {
            try
            {
                if (id != user.IdAutentication)
                {
                    return BadRequest("Dados inválidos");
                }

                var updateUser = await _repository.UpdateUser(user);
                return Ok(updateUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar usuário: {ex.Message}");
            }
        }

        [HttpDelete("{id:minlength(3):maxlength(100)}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            try
            {
                var deleteUser = await _repository.DeleteUser(id);

                if (deleteUser == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok(deleteUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao deletar usuário: {ex.Message}");
            }
        }
    }
}