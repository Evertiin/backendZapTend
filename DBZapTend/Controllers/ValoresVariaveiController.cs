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
    [Authorize(Roles = "User,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ValoresVariaveiController : Controller
    {
        private readonly IValoresVariaveiRepository _repository;

        public ValoresVariaveiController(IValoresVariaveiRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValoresVariavei>>> GetValoresVariaveis()
        {
            try
            {
                var valores = await _repository.GetValoresVariaveis();
                await Log.LogToFile("log_", "COD:1009-2 ,Valores variáveis coletados com sucesso");
                return Ok(new { Message = "COD:1009-2 ,Valores variáveis coletados com sucesso", Valores = valores });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", $"COD:1009-5 ,Erro interno ao buscar valores variáveis");
                return StatusCode(500, $"COD:1009-5 ,Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ValoresVariavei>> CreateValoresVariaveis(ValoresVariavei valores)
        {
            try
            {
                if (valores is null)
                {
                    await Log.LogToFile("log_", "COD:1009-4 ,Dados inválidos ao criar valores variáveis");
                    return BadRequest("COD:1009-4 ,Dados inválidos");
                }

                var createdValores = await _repository.CreateValoresVariaveis(valores);
                await Log.LogToFile("log_", $"COD:1009-2 ,Valores variáveis criados com sucesso");
                return Ok(new { Message = "COD:1009-2 ,Valores variáveis criados com sucesso", Valores = createdValores });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", $"COD:1009-5 ,Erro interno ao criar valores variáveis");
                return StatusCode(500, $"COD:1009-5 ,Erro interno do servidor");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ValoresVariavei>> GetValoresVariaveisId(int id)
        {
            try
            {
                var valores = await _repository.GetValoresVariavei(id);

                if (valores == null)
                {
                    await Log.LogToFile("log_", $"COD:1009-4 ,Valor variável não encontrado: {id}");
                    return NotFound("COD:1009-4 ,Valor variável não encontrado");
                }

                await Log.LogToFile("log_", $"COD:1009-2 ,Valor variável coletado com sucesso: {id}");
                return Ok(new { Message = "COD:1009-2 ,Valor variável coletado com sucesso", Valores = valores });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", $"COD:1009-5 ,Erro interno ao buscar valor variável por ID");
                return StatusCode(500, $"COD:1009-5 ,Erro interno do servidor");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<ValoresVariavei>> UpdateValoresVariaveis(int id, UpdateValoresVariaveiDto valores)
        {
            try
            {
                var findVariavel = await _repository.GetValoresVariavei(id);

                if (findVariavel is null)
                {
                    await Log.LogToFile("log_", $"COD:1009-4 ,Valor variável não encontrado: {id}");
                    return NotFound("COD:1009-4 ,Valor variável não encontrado");
                }

                if (!string.IsNullOrWhiteSpace(valores.Value))
                {
                    findVariavel.Value = valores.Value;
                }

                if (valores.VariaveisIdVariaveis.HasValue)
                {
                    findVariavel.VariaveisIdVariaveis = valores.VariaveisIdVariaveis.Value;
                }

                if (valores.PromptsIdPrompts.HasValue)
                {
                    findVariavel.PromptsIdPrompts = valores.PromptsIdPrompts.Value;
                }

                await _repository.UpdateValoresVariaveis(findVariavel);
                await Log.LogToFile("log_", $"COD:1009-2 ,Valor variável atualizado com sucesso: {id}");
                return Ok(new { Message = "COD:1009-2 ,Valor variável atualizado com sucesso", Valores = findVariavel });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", $"COD:1009-5 ,Erro interno ao atualizar valores variáveis");
                return StatusCode(500, $"COD:1009-5 ,Erro interno do servidor");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ValoresVariavei>> DeleteValoresVariaveis(int id)
        {
            try
            {
                var valores = await _repository.GetValoresVariavei(id);

                if (valores == null)
                {
                    await Log.LogToFile("log_", $"COD:1009-4 ,Valor variável não encontrado: {id}");
                    return NotFound("COD:1009-4 ,Valor variável não encontrado");
                }

                var deleteValores = await _repository.DeleteValoresVariaveis(id);
                await Log.LogToFile("log_", $"COD:1009-2 ,Valor variável deletado com sucesso: {id}");
                return Ok(new { Message = "COD:1009-2 ,Valor variável deletado com sucesso", Valores = deleteValores });
            }
            catch (Exception)
            {
                await Log.LogToFile("log_", $"COD:1009-5 ,Erro interno ao deletar valores variáveis");
                return StatusCode(500, $"COD:1009-5 ,Erro interno do servidor");
            }
        }
    }
}