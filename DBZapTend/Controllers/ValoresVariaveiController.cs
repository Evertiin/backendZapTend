using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

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
            var valores = await _repository.GetValoresVariaveis();
            return Ok(valores);
        }

        [HttpPost]
        public async Task<ActionResult<ValoresVariavei>> CreateValoresVariaveis(ValoresVariavei valores)
        {
            if (valores is null)
            {
                return BadRequest("Dados inválidos");
            }
            var createdValores = await _repository.CreateValoresVariaveis(valores);


            return Ok(createdValores);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<ValoresVariavei>> GetValoresVariaveisId(int id)
        {
            var valores = await _repository.GetValoresVariavei(id);

            if (valores == null)
            {
                return NotFound("Variavel não encontrada");
            }


            return Ok(valores);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ValoresVariavei>> UpdateValoresVariaveis(int id, ValoresVariavei valores)
        {
            if (id != valores.IdValoresVariaveis)
            {
                return BadRequest("Dados inválidos");
            }

            var updateValores = await _repository.CreateValoresVariaveis(valores);


            return Ok(updateValores);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ValoresVariavei>> DeleteValoresVariaveis(int id)
        {
            var valores = _repository.GetValoresVariavei(id);

            if (valores == null)
            {
                return NotFound("Variavel não encontrada");
            }
            var deleteValores = await _repository.DeleteValoresVariaveis(id);


            return Ok(deleteValores);
        }
    }
}
