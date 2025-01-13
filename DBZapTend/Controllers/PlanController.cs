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
            try
            {
                var plan = await _repository.GetPlans();
                return Ok(plan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar planos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> CreatePlan(Plan plan)
        {
            try
            {
                if (plan is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var createdPlan = await _repository.CreatePlan(plan);
                return Ok(createdPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao criar plano: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Plan>> GetPlanId(int id)
        {
            try
            {
                var plan = await _repository.GetPlan(id);

                if (plan == null)
                {
                    return NotFound("Plano não encontrado");
                }

                return Ok(plan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar plano por ID: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Plan>> UpdatePlan(int id, Plan plan)
        {
            try
            {
                if (id != plan.Id)
                {
                    return BadRequest("Dados inválidos");
                }

                var updatePlan = await _repository.UpdatePlan(plan);
                return Ok(updatePlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar plano: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Plan>> DeletePlan(int id)
        {
            try
            {
                var plan = await _repository.GetPlan(id);

                if (plan == null)
                {
                    return NotFound("Plano não encontrado");
                }

                var deletePlan = await _repository.DeletePlan(id);
                return Ok(deletePlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao deletar plano: {ex.Message}");
            }
        }
    }
}