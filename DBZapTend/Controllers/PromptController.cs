using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

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
            var prompt = await _repository.GetPrompts();
            return Ok(prompt);
        }

        [HttpPost]
        public async Task<ActionResult<Prompt>> CreatePrompt(Prompt prompt)
        {
            if (prompt is null)
            {
                return BadRequest("Dados inválidos");
            }
            var createdPrompt = await _repository.CreatePrompt(prompt);


            return Ok(createdPrompt);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Prompt>> GetPromptId(int id)
        {
            var prompt = await _repository.GetPrompt(id);

            if (prompt == null)
            {
                return NotFound("Usuário não encontrado");
            }


            return Ok(prompt);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Prompt>> UpdatePrompt(int id, Prompt prompt)
        {
            if (id != prompt.IdPrompts)
            {
                return BadRequest("Dados inválidos");
            }

            var updatePrompt = await _repository.UpdatePrompt(prompt);


            return Ok(updatePrompt);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Prompt>> DeletePrompt(int id)
        {
            var deletePrompt = await _repository.DeletePrompt(id);
            if (deletePrompt == null)
                return NotFound("Usuário não encontrado");

            return Ok(deletePrompt);
        }

    }
}
