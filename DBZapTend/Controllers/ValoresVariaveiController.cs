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
                return Ok(valores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar valores variáveis: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ValoresVariavei>> CreateValoresVariaveis(ValoresVariavei valores)
        {
            try
            {
                if (valores is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var createdValores = await _repository.CreateValoresVariaveis(valores);
                return Ok(createdValores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao criar valores variáveis: {ex.Message}");
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
                    return NotFound("Variável não encontrada");
                }

                return Ok(valores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar valor variável por ID: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ValoresVariavei>> UpdateValoresVariaveis(int id, ValoresVariavei valores)
        {
            try
            {
                if (id != valores.IdValoresVariaveis)
                {
                    return BadRequest("Dados inválidos");
                }

                var updateValores = await _repository.UpdateValoresVariaveis(valores);
                return Ok(updateValores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar valores variáveis: {ex.Message}");
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
                    return NotFound("Variável não encontrada");
                }

                var deleteValores = await _repository.DeleteValoresVariaveis(id);
                return Ok(deleteValores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao deletar valores variáveis: {ex.Message}");
            }
        }
    }
}