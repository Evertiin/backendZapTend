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
                return Ok(variaveis);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar variáveis: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Variavei>> CreateVariavel(Variavei variavei)
        {
            try
            {
                if (variavei is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var createdVariavel = await _repository.CreateVariavei(variavei);
                return Ok(createdVariavel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao criar variável: {ex.Message}");
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
                    return NotFound("Variável não encontrada");
                }

                return Ok(variavei);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar variável por ID: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Variavei>> UpdateVariavel(int id, Variavei variavei)
        {
            try
            {
                if (id != variavei.IdVariaveis)
                {
                    return BadRequest("Dados inválidos");
                }

                var updateVariavel = await _repository.UpdateVariavei(variavei);
                return Ok(updateVariavel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar variável: {ex.Message}");
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
                    return NotFound("Variável não encontrada");
                }

                var deleteVariavel = await _repository.DeleteVariavei(id);
                return Ok(deleteVariavel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao deletar variável: {ex.Message}");
            }
        }
    }
}