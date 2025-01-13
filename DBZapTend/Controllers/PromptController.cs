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
    public class PromptController : Controller
    {
        private readonly IPromptRepository _repository;

        public PromptController(IPromptRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prompt>>> Get()
        {
            try
            {
                var prompt = await _repository.GetPrompts();
                return Ok(prompt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar prompts: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Prompt>> CreatePrompt(Prompt prompt)
        {
            try
            {
                if (prompt is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var createdPrompt = await _repository.CreatePrompt(prompt);
                return Ok(createdPrompt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao criar prompt: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Prompt>> GetPromptId(int id)
        {
            try
            {
                var prompt = await _repository.GetPrompt(id);

                if (prompt == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok(prompt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar prompt por ID: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Prompt>> UpdatePrompt(int id, Prompt prompt)
        {
            try
            {
                if (id != prompt.IdPrompts)
                {
                    return BadRequest("Dados inválidos");
                }

                var updatePrompt = await _repository.UpdatePrompt(prompt);
                return Ok(updatePrompt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar prompt: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Prompt>> DeletePrompt(int id)
        {
            try
            {
                var deletePrompt = await _repository.DeletePrompt(id);

                if (deletePrompt == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok(deletePrompt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao deletar prompt: {ex.Message}");
            }
        }
    }
}