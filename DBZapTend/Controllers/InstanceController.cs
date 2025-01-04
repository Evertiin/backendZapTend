using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DBZapTend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstanceController : Controller
    {
        private readonly IInstanceRepository _repository;

        public InstanceController(IInstanceRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instance>>> GetInstances()
        {
            var instance = await _repository.GetInstances();
            return Ok(instance);
        }

        [HttpPost]
        public async Task<ActionResult<Instance>> CreateInstance(Instance instance)
        {
            if (instance is null)
            {
                return BadRequest("Dados inválidos");
            }
            var createdInstance = await _repository.CreateInstance(instance);


            return Ok(createdInstance);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Instance>> GetInstanceId(int id)
        {
            var instance = await _repository.GetInstance(id);

            if (instance == null)
            {
                return NotFound("Instancia não encontrado");
            }


            return Ok(instance);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Instance>> UpdateInstance(int id, Instance instance)
        {
            if (id != instance.Id)
            {
                return BadRequest("Dados inválidos");
            }

            var updateInstance = await _repository.CreateInstance(instance);


            return Ok(updateInstance);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Instance>> DeleteInstance(int id)
        {
            var instance = _repository.GetInstance(id);

            if (instance == null)
            {
                return NotFound("Instancia não encontrada");
            }
            var deleteUser = await _repository.DeleteInstance(id);


            return Ok(deleteUser);
        }
    }
}
