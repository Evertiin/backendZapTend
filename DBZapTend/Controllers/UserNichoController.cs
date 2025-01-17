using DBZapTend.Logs;
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
                await Log.LogToFile("log_", "COD:1008-2 ,UserNichos coletados com sucesso");
                return Ok(new { Message = "COD:1008-2 ,UserNichos coletados com sucesso", UserNichos = userNichos });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1008-5 ,Erro interno ao buscar UserNichos: {ex.Message}");
                return StatusCode(500, $"COD:1008-5 ,Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserNicho>> CreateUserNicho(UserNicho userNicho)
        {
            try
            {
                if (userNicho is null)
                {
                    await Log.LogToFile("log_", "COD:1008-4 ,Dados inválidos ao criar UserNicho");
                    return BadRequest("COD:1008-4 ,Dados inválidos");
                }

                var createdUserNicho = await _repository.CreateUserNicho(userNicho);
                await Log.LogToFile("log_", $"COD:1008-2 ,UserNicho criado com sucesso");
                return Ok(new { Message = "COD:1008-2 ,UserNicho criado com sucesso", UserNicho = createdUserNicho });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1008-5 ,Erro interno ao criar UserNicho: {ex.Message}");
                return StatusCode(500, $"COD:1008-5 ,Erro interno do servidor");
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
                    await Log.LogToFile("log_", $"COD:1008-4 ,UserNicho não encontrado: {id}");
                    return NotFound("COD:1008-4 ,UserNicho não encontrado");
                }

                await Log.LogToFile("log_", $"COD:1008-2 ,UserNicho coletado com sucesso: {id}");
                return Ok(new { Message = "COD:1008-2 ,UserNicho coletado com sucesso", UserNicho = userNicho });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1008-5 ,Erro interno ao buscar UserNicho por ID: {ex.Message}");
                return StatusCode(500, $"COD:1008-5 ,Erro interno do servidor");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserNicho>> UpdateUserNichos(int id, UserNicho userNicho)
        {
            try
            {
                if (id != userNicho.IdUserNichos)
                {
                    await Log.LogToFile("log_", "COD:1008-4 ,Dados inválidos ao atualizar UserNicho");
                    return BadRequest("COD:1008-4 ,Dados inválidos");
                }

                var updatedUserNicho = await _repository.UpdateUserNicho(userNicho);
                await Log.LogToFile("log_", $"COD:1008-2 ,UserNicho atualizado com sucesso: {id}");
                return Ok(new { Message = "COD:1008-2 ,UserNicho atualizado com sucesso", UserNicho = updatedUserNicho });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1008-5 ,Erro interno ao atualizar UserNicho: {ex.Message}");
                return StatusCode(500, $"COD:1008-5 ,Erro interno do servidor");
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
                    await Log.LogToFile("log_", $"COD:1008-4 ,UserNicho não encontrado: {id}");
                    return NotFound("COD:1008-4 ,UserNicho não encontrado");
                }

                var deleteUserNicho = await _repository.DeleteUserNicho(id);
                await Log.LogToFile("log_", $"COD:1008-2 ,UserNicho deletado com sucesso: {id}");
                return Ok(new { Message = "COD:1008-2 ,UserNicho deletado com sucesso", UserNicho = deleteUserNicho });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1008-5 ,Erro interno ao deletar UserNicho: {ex.Message}");
                return StatusCode(500, $"COD:1008-5 ,Erro interno do servidor");
            }
        }
    }
}