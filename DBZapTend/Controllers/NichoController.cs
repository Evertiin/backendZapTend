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
    //[Authorize(Roles = "User,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class NichoController : Controller
    {
        private readonly INichosRepository _repository;

        public NichoController(INichosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nicho>>> GetNichos()
        {
            try
            {
                var nichos = await _repository.GetNichos();
                await Log.LogToFile("log_", "COD:1004-2 ,Nichos coletados com sucesso");
                return Ok(new { Message = "COD:1004-2 ,Nichos coletados com sucesso", Nichos = nichos });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1004-5 ,Erro interno ao buscar nichos: {ex.Message}");
                return StatusCode(500, $"COD:1004-5 ,Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Nicho>> CreateNicho(Nicho nicho)
        {
            try
            {
                if (nicho is null)
                {
                    await Log.LogToFile("log_", "COD:1004-4 ,Dados inválidos ao criar nicho");
                    return BadRequest("COD:1004-4 ,Dados inválidos");
                }

                var createdNicho = await _repository.CreateNicho(nicho);
                await Log.LogToFile("log_", $"COD:1004-2 ,Nicho criado com sucesso");
                return Ok(new { Message = "COD:1004-2 ,Nicho criado com sucesso", Nicho = createdNicho });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1004-5 ,Erro interno ao criar nicho: {ex.Message}");
                return StatusCode(500, $"COD:1004-5 ,Erro interno do servidor");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Nicho>> GetNichoId(int id)
        {
            try
            {
                var nicho = await _repository.GetNicho(id);

                if (nicho == null)
                {
                    await Log.LogToFile("log_", $"COD:1004-4 ,Nicho não encontrado: {id}");
                    return NotFound("COD:1004-4 ,Nicho não encontrado");
                }

                await Log.LogToFile("log_", $"COD:1004-2 ,Nicho coletado com sucesso: {id}");
                return Ok(new { Message = "COD:1004-2 ,Nicho coletado com sucesso", Nicho = nicho });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1004-5 ,Erro interno ao buscar nicho por ID: {ex.Message}");
                return StatusCode(500, $"COD:1004-5 ,Erro interno do servidor");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Nicho>> UpdateNicho(int id, Nicho nicho)
        {
            try
            {
                if (id != nicho.IdNichos)
                {
                    await Log.LogToFile("log_", "COD:1004-4 ,Dados inválidos ao atualizar nicho");
                    return BadRequest("COD:1004-4 ,Dados inválidos");
                }

                var updatedNicho = await _repository.UpdateNicho(nicho);
                await Log.LogToFile("log_", $"COD:1004-2 ,Nicho atualizado com sucesso: {id}");
                return Ok(new { Message = "COD:1004-2 ,Nicho atualizado com sucesso", Nicho = updatedNicho });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1004-5 ,Erro interno ao atualizar nicho: {ex.Message}");
                return StatusCode(500, $"COD:1004-5 ,Erro interno do servidor");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Nicho>> DeleteNicho(int id)
        {
            try
            {
                var nicho = await _repository.GetNicho(id);

                if (nicho == null)
                {
                    await Log.LogToFile("log_", $"COD:1004-4 ,Nicho não encontrado: {id}");
                    return NotFound("COD:1004-4 ,Nicho não encontrado");
                }

                var deleteNicho = await _repository.DeleteNicho(id);
                await Log.LogToFile("log_", $"COD:1004-2 ,Nicho deletado com sucesso: {id}");
                return Ok(new { Message = "COD:1004-2 ,Nicho deletado com sucesso", Nicho = deleteNicho });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1004-5 ,Erro interno ao deletar nicho: { ex.Message}");
                return StatusCode(500, $"COD:1004-5 ,Erro interno do servidor");
            }
        }
    }
}