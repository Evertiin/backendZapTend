using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBZapTend.Controllers
{
    [Authorize(Roles = "User,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserNichoController : Controller
    {
        private readonly IUserNichoRepository _repository;

        public UserNichoController(IUserNichoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNicho>>> GetUserNichos()
        {
            try
            {
                var userNichos = await _repository.GetUserNichos();
                return Ok(userNichos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar UserNichos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserNicho>> CreateUserNicho(UserNicho userNicho)
        {
            try
            {
                if (userNicho is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var createdUserNicho = await _repository.CreateUserNicho(userNicho);
                return Ok(createdUserNicho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao criar UserNicho: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserNicho>> GetUserNichoId(int id)
        {
            try
            {
                var userNicho = await _repository.GetUserNicho(id);

                if (userNicho == null)
                {
                    return NotFound("UserNicho não encontrado");
                }

                return Ok(userNicho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar UserNicho por ID: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserNicho>> UpdateUserNichos(int id, UserNicho userNicho)
        {
            try
            {
                if (id != userNicho.IdUserNichos)
                {
                    return BadRequest("Dados inválidos");
                }

                var updateUserNicho = await _repository.UpdateUserNicho(userNicho);
                return Ok(updateUserNicho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar UserNicho: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserNicho>> DeleteUserNichos(int id)
        {
            try
            {
                var userNicho = await _repository.GetUserNicho(id);

                if (userNicho == null)
                {
                    return NotFound("UserNicho não encontrado");
                }

                var deleteUserNicho = await _repository.DeleteUserNicho(id);
                return Ok(deleteUserNicho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao deletar UserNicho: {ex.Message}");
            }
        }
    }
}