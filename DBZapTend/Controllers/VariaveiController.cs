using DBZapTend.DTO;
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
    public class VariaveiController : Controller
    {
        private readonly IVariaveiRepository _repository;

        public VariaveiController(IVariaveiRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Variavei>>> Get()
        {
            try
            {
                var variaveis = await _repository.GetVariaveis();
                await Log.LogToFile("log_", "COD:1010-2 ,Variáveis coletadas com sucesso");
                return Ok(new { Message = "COD:1010-2 ,Variáveis coletadas com sucesso", Variaveis = variaveis });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", $"COD:1010-5 ,Erro interno ao buscar variáveis");
                return StatusCode(500, $"COD:1010-5 ,Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Variavei>> CreateVariavel(Variavei variavei)
        {
            try
            {
                if (variavei is null)
                {
                    await Log.LogToFile("log_", "COD:1010-4 ,Dados inválidos ao criar variável");
                    return BadRequest("COD:1010-4 ,Dados inválidos");
                }

                var createdVariavel = await _repository.CreateVariavei(variavei);
                await Log.LogToFile("log_", $"COD:1010-2 ,Variável criada com sucesso");
                return Ok(new { Message = "COD:1010-2 ,Variável criada com sucesso", Variavel = createdVariavel });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", "COD:1010-5 ,Erro interno ao criar variável");
                return StatusCode(500, $"COD:1010-5 ,Erro interno do servidor");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Variavei>> GetVariavelId(int id)
        {
            try
            {
                var variavei = await _repository.GetVariavei(id);

                if (variavei == null)
                {
                    await Log.LogToFile("log_", $"COD:1010-4 ,Variável não encontrada: {id}");
                    return NotFound("COD:1010-4 ,Variável não encontrada");
                }

                await Log.LogToFile("log_", $"COD:1010-2 ,Variável coletada com sucesso: {id}");
                return Ok(new { Message = "COD:1010-2 ,Variável coletada com sucesso", Variavel = variavei });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", $"COD:1010-5 ,Erro interno ao buscar variável por ID");
                return StatusCode(500, $"COD:1010-5 ,Erro interno do servidor");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Variavei>> UpdateVariavel(int id, [FromBody] UpdateVariaveiDto variavei)
        {
            try
            {
                var findVariavel = await _repository.GetVariavei(id);

                if (findVariavel is null)
                {
                    await Log.LogToFile("log_", $"COD:1010-4 ,Variável não encontrada: {id}");
                    return NotFound("COD:1010-4 ,Variável não encontrada");
                }

                if (!string.IsNullOrWhiteSpace(variavei.Name))
                {
                    findVariavel.Name = variavei.Name;
                }

                if (!string.IsNullOrWhiteSpace(variavei.Description))
                {
                    findVariavel.Description = variavei.Description;
                }

                await _repository.UpdateVariavei(findVariavel);
                await Log.LogToFile("log_", $"COD:1010-2 ,Variável atualizada com sucesso: {id}");
                return Ok(new { Message = "COD:1010-2 ,Variável atualizada com sucesso", Variavel = findVariavel });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", $"COD:1010-5 ,Erro interno ao atualizar variável");
                return StatusCode(500, $"COD:1010-5 ,Erro interno do servidor");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Variavei>> DeleteVariavel(int id)
        {
            try
            {
                var variavel = await _repository.GetVariavei(id);

                if (variavel == null)
                {
                    await Log.LogToFile("log_", $"COD:1010-4 ,Variável não encontrada: {id}");
                    return NotFound("COD:1010-4 ,Variável não encontrada");
                }

                var deleteVariavel = await _repository.DeleteVariavei(id);
                await Log.LogToFile("log_", $"COD:1010-2 ,Variável deletada com sucesso: {id}");
                return Ok(new { Message = "COD:1010-2 ,Variável deletada com sucesso", Variavel = deleteVariavel });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", $"COD:1010-5 ,Erro interno ao deletar variável");
                return StatusCode(500, $"COD:1010-5 ,Erro interno do servidor");
            }
        }
    }
}