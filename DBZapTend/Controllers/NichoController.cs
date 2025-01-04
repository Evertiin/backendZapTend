using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DBZapTend.Controllers
{
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
            var nicho = await _repository.GetNichos();
            return Ok(nicho);
        }

        [HttpPost]
        public async Task<ActionResult<Nicho>> CreateNicho(Nicho nicho)
        {
            if (nicho is null)
            {
                return BadRequest("Dados inválido");
            }

            var createdNicho = await _repository.CreateNicho(nicho);


            return Ok(createdNicho);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Nicho>> GetNichoId(int id)
        {
            var nicho = await _repository.GetNicho(id);

            if (nicho == null)
            {
                return NotFound("Nicho não encontrado");
            }


            return Ok(nicho);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Nicho>> UpdateNicho(int id, Nicho nicho)
        {
            if (id != nicho.IdNichos)
            {
                return BadRequest("Dados inválidos");
            }

            var updateNicho = await _repository.UpdateNicho(nicho);


            return Ok(updateNicho);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Nicho>> DeleteNicho(int id)
        {
            var nicho = _repository.GetNicho(id);

            if (nicho == null)
            {
                return NotFound("Nicho não encontrado");
            }
            var deleteNicho = await _repository.DeleteNicho(id);


            return Ok(deleteNicho);
        }


    }
}
