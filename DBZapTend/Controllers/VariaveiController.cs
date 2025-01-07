using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

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
            var variavel = await _repository.GetVariaveis();
            return Ok(variavel);
        }

        [HttpPost]
        public async Task<ActionResult<Variavei>> CreateVariavel(Variavei variavei)
        {
            if (variavei is null)
            {
                return BadRequest("Dados inválidos");
            }

            var createdVariavel = await _repository.CreateVariavei(variavei);


            return Ok(createdVariavel);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Variavei>> GetVariavelId(int id)
        {
            var variavei = await _repository.GetVariavei(id);

            if (variavei == null)
            {
                return NotFound("Variavel não encontrada");
            }


            return Ok(variavei);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Variavei>> UpdateVariavel(int id, Variavei variavei)
        {
            if (id != variavei.IdVariaveis)
            {
                return BadRequest("Dados inválidos");
            }

            var updateVariavel = await _repository.UpdateVariavei(variavei);


            return Ok(updateVariavel);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Variavei>> DeleteVariavel(int id)
        {
            var variavel = _repository.GetVariavei(id);

            if (variavel == null)
            {
                return NotFound("Variavel não encontrada");
            }
            var deleteVariavel = await _repository.DeleteVariavei(id);


            return Ok(deleteVariavel);
        }
    }
}
