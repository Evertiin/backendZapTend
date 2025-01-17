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
    [Authorize(Roles = "Admin")]
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
                var plans = await _repository.GetPlans();
                await Log.LogToFile("log_", "COD:1006-2 ,Planos coletados com sucesso");
                return Ok(new { Message = "COD:1006-2 ,Planos coletados com sucesso", Plans = plans });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1006-5 ,Erro interno ao buscar planos: {ex.Message}");
                return StatusCode(500, $"COD:1006-5 ,Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> CreatePlan(Plan plan)
        {
            try
            {
                if (plan is null)
                {
                    await Log.LogToFile("log_", "COD:1006-4 ,Dados inválidos ao criar plano");
                    return BadRequest("COD:1006-4 ,Dados inválidos");
                }

                var createdPlan = await _repository.CreatePlan(plan);
                await Log.LogToFile("log_", $"COD:1006-2 ,Plano criado com sucesso");
                return Ok(new { Message = "COD:1006-2 ,Plano criado com sucesso", Plan = createdPlan });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1006-5 ,Erro interno ao criar plano: {ex.Message}");
                return StatusCode(500, $"COD:1006-5 ,Erro interno do servidor");
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
                    await Log.LogToFile("log_", $"COD:1006-4 ,Plano não encontrado: {id}");
                    return NotFound("COD:1006-4 ,Plano não encontrado");
                }

                await Log.LogToFile("log_", $"COD:1006-2 ,Plano coletado com sucesso: {id}");
                return Ok(new { Message = "COD:1006-2 ,Plano coletado com sucesso", Plan = plan });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1006-5 ,Erro interno ao buscar plano por ID: {ex.Message}");
                return StatusCode(500, $"COD:1006-5 ,Erro interno do servidor");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Plan>> UpdatePlan(int id, Plan plan)
        {
            try
            {
                if (id != plan.Id)
                {
                    await Log.LogToFile("log_", "COD:1006-4 ,Dados inválidos ao atualizar plano");
                    return BadRequest("COD:1006-4 ,Dados inválidos");
                }

                var updatedPlan = await _repository.UpdatePlan(plan);
                await Log.LogToFile("log_", $"COD:1006-2 ,Plano atualizado com sucesso: {id}");
                return Ok(new { Message = "COD:1006-2 ,Plano atualizado com sucesso", Plan = updatedPlan });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1006-5 ,Erro interno ao atualizar plano: {ex.Message}");
                return StatusCode(500, $"COD:1006-5 ,Erro interno do servidor");
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
                    await Log.LogToFile("log_", $"COD:1006-4 ,Plano não encontrado: {id}");
                    return NotFound("COD:1006-4 ,Plano não encontrado");
                }

                var deletePlan = await _repository.DeletePlan(id);
                await Log.LogToFile("log_", $"COD:1006-2 ,Plano deletado com sucesso: {id}");
                return Ok(new { Message = "COD:1006-2 ,Plano deletado com sucesso", Plan = deletePlan });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1006-5 ,Erro interno ao deletar plano: {ex.Message}");
                return StatusCode(500, $"COD:1006-5 ,Erro interno do servidor");
            }
        }
    }
}