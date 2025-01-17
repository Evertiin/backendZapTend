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
                var prompts = await _repository.GetPrompts();
                await Log.LogToFile("log_", "COD:1007-2 ,Prompts coletados com sucesso");
                return Ok(new { Message = "COD:1007-2 ,Prompts coletados com sucesso", Prompts = prompts });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1007-5 ,Erro interno ao buscar prompts: {ex.Message}");
                return StatusCode(500, $"COD:1007-5 ,Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Prompt>> CreatePrompt(Prompt prompt)
        {
            try
            {
                if (prompt is null)
                {
                    await Log.LogToFile("log_", "COD:1007-4 ,Dados inválidos ao criar prompt");
                    return BadRequest("COD:1007-4 ,Dados inválidos");
                }

                var createdPrompt = await _repository.CreatePrompt(prompt);
                await Log.LogToFile("log_", $"COD:1007-2 ,Prompt criado com sucesso");
                return Ok(new { Message = "COD:1007-2 ,Prompt criado com sucesso", Prompt = createdPrompt });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1007-5 ,Erro interno ao criar prompt: {ex.Message}");
                return StatusCode(500, $"COD:1007-5 ,Erro interno do servidor");
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
                    await Log.LogToFile("log_", $"COD:1007-4 ,Prompt não encontrado: {id}");
                    return NotFound("COD:1007-4 ,Prompt não encontrado");
                }

                await Log.LogToFile("log_", $"COD:1007-2 ,Prompt coletado com sucesso: {id}");
                return Ok(new { Message = "COD:1007-2 ,Prompt coletado com sucesso", Prompt = prompt });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1007-5 ,Erro interno ao buscar prompt por ID: {ex.Message}");
                return StatusCode(500, $"COD:1007-5 ,Erro interno do servidor");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Prompt>> UpdatePrompt(int id, [FromBody] UpdatePromptDto prompt)
        {
            try
            {
                var findPrompt = await _repository.GetPrompt(id);

                if (findPrompt is null)
                {
                    await Log.LogToFile("log_", $"COD:1007-4 ,Prompt não encontrado: {id}");
                    return NotFound("COD:1007-4 ,Prompt não encontrado");
                }

                if (!string.IsNullOrWhiteSpace(prompt.Title))
                {
                    findPrompt.Title = prompt.Title;
                }

                if (!string.IsNullOrWhiteSpace(prompt.Conteudo))
                {
                    findPrompt.Conteudo = prompt.Conteudo;
                }

                if (prompt.NichosIdNichos.HasValue)
                {
                    findPrompt.NichosIdNichos = prompt.NichosIdNichos.Value;
                }

                await _repository.UpdatePrompt(findPrompt);
                await Log.LogToFile("log_", $"COD:1007-2 ,Prompt atualizado com sucesso: {id}");
                return Ok(new { Message = "COD:1007-2 ,Prompt atualizado com sucesso", Prompt = findPrompt });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1007-5 ,Erro interno ao atualizar prompt: {ex.Message}");
                return StatusCode(500, $"COD:1007-5 ,Erro interno do servidor");
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
                    await Log.LogToFile("log_", $"COD:1007-4 ,Prompt não encontrado: {id}");
                    return NotFound("COD:1007-4 ,Prompt não encontrado");
                }

                await Log.LogToFile("log_", $"COD:1007-2 ,Prompt deletado com sucesso: {id}");
                return Ok(new { Message = "COD:1007-2 ,Prompt deletado com sucesso", Prompt = deletePrompt });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1007-5 ,Erro interno ao deletar prompt: {ex.Message}");
                return StatusCode(500, $"COD:1007-5 ,Erro interno do servidor");
            }
        }
    }
}