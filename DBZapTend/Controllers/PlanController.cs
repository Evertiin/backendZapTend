using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DBZapTend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : Controller
    {
        private readonly IPlanRepository _repository;

        public PlanController(IPlanRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlans()
        {
            var plan = await _repository.GetPlans();
            return Ok(plan);
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> CreatePlan(Plan plan)
        {
            if (plan is null)
            {
                return BadRequest("Dados inválidos");
            }
            var createdPlan = await _repository.CreatePlan(plan);


            return Ok(createdPlan);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Plan>> GetPlanId(int id)
        {
            var plan = await _repository.GetPlan(id);

            if (plan == null)
            {
                return NotFound("Plano não encontrado");
            }


            return Ok(plan);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Plan>> UpdatePlan(int id, Plan plan)
        {
            if (id != plan.Id)
            {
                return BadRequest("Dados inválidos");
            }

            var updatePlan = await _repository.UpdatePlan(plan);


            return Ok(updatePlan);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Plan>> DeletePlan(int id)
        {
            var plan = _repository.GetPlan(id);

            if (plan == null)
            {
                return NotFound("Plano não encontrado");
            }
            var deletePlan = await _repository.DeletePlan(id);


            return Ok(deletePlan);
        }
    }
}
